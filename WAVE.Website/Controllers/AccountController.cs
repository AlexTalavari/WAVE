using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WAVE.Dal.Entities;
using WAVE.Dal.Infrastructure;
using WAVE.Dal.Repositories;
using WAVE.Website.Filters;
using WAVE.Website.Models;
using WebMatrix.WebData;

namespace WAVE.Website.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : CultureController
    {
        private readonly IRepository<Account> _accountRepository;
        private readonly IRepository<EmailActivation> _emailActivationRepository;
        private readonly IRepository<ImageData> _imageDataActivationRepository;
        private readonly IRepository<RestrictedUserNames> _restricteUserNamesRepository;
        private readonly IRepository<Term> _termRepository;
        private readonly IRepository<User> _userRepository;

        public AccountController()
        {
            _accountRepository = new Repository<Account>();
            _userRepository = new Repository<User>();
            _termRepository = new Repository<Term>();
            _emailActivationRepository = new Repository<EmailActivation>();
            _imageDataActivationRepository = new Repository<ImageData>();
            _restricteUserNamesRepository = new Repository<RestrictedUserNames>();
        }

        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            BaseModel.Title = "Log In";
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            var rgx = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" +
                         @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
            var regex = new Regex(rgx);
            var match = regex.Match(model.UserName);
            if (match.Success)
            {
                model.UserName = _accountRepository.FindBy(a => a.Email == model.UserName).UserName;
            }

            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, model.RememberMe))
                //&&_userRepository.Get(u=>u.Account.UserName==model.UserName).First().Activated)
            {
                if (Roles.IsUserInRole(model.UserName, "Banned"))
                {
                    WebSecurity.Logout();
                    return RedirectToAction("Index", "Home");
                }
                if (returnUrl == null || returnUrl == "")
                {
                    return RedirectToAction("Index", "User", new {@userName = model.UserName});
                }
                return RedirectToLocal(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("",
                "The user name or password provided is incorrect or your account pends activation.");
            return View(model);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index", "Home");
        }


        //
        // GET: /Account/Register/

        [AllowAnonymous]
        public ActionResult Register(String Id, RegisterModel model)
        {
            if (model == null)
            {
                model = Mapper.Map<RegisterModel>(BaseModel);
                model.Title = "Register";
            }

            if (Request.IsAjaxRequest())
            {
                model.UserProfile.UserType = Id;
                return PartialView("~/Views/Account/SignupPartials/_RegisterAccount.cshtml", model);
            }
            return View(model);
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            // Check if role exists 
            if (!Roles.RoleExists("User"))
            {
                Roles.CreateRole("User");
            }
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    var count = _restricteUserNamesRepository.FilterBy(run => run.Data == model.UserName).Count();
                    if (count <= 0)
                    {
                        //Create acc
                        WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                        //Give him role
                        Roles.AddUserToRole(model.UserName, "User");

                        //Create Userprofile
                        var accId = WebSecurity.GetUserId(model.UserName);
                        model.UserProfile.Terms =
                            _termRepository.All().OrderByDescending(t => t.DateModified).FirstOrDefault();
                        model.UserProfile.DateCreated = DateTime.Now;
                        model.UserProfile.DateModified = DateTime.Now;
                        model.UserProfile.AgreedToTermsDate = DateTime.Now;
                        model.UserProfile.Account = _accountRepository.FindBy(a => a.Id == accId);
                        model.UserProfile.Account.Email = model.Email;
                        model.UserProfile.Name = model.Name;
                        model.UserProfile.Surname = model.Surname;
                        model.UserProfile.ProfilePhoto = _imageDataActivationRepository.FindBy(i => i.Id == 1);
                        model.UserProfile.BannerPhoto = _imageDataActivationRepository.FindBy(i => i.Id == 2);
                        _userRepository.Add(model.UserProfile);

                        var guid = Guid.NewGuid();
                        //                        _emailActivationRepository.SaveOrUpdate(new EmailActivation { Email = model.Email, Code = guid, Activated = false });

                        var email = model.Email;
                        var subj = "Welcome to WAVE";
                        var reader = new StreamReader(Server.MapPath("~/Templates/Email/Welcome.html"));
                        var msg = reader.ReadToEnd();

                        //Here replace the name with [MyName]
                        msg = msg.Replace("[MyName]", model.Name + " " + model.Surname);

                        //Email.SendEmail(email, subj, msg);
                        //this.Session["NotificationMessage"] = "Account registered Successfully";
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        //
        // POST: /Account/Disassociate

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Disassociate(string provider, string providerUserId)
        {
            var ownerAccount = OAuthWebSecurity.GetUserName(provider, providerUserId);
            ManageMessageId? message = null;

            // Only disassociate the account if the currently logged in user is the owner
            if (ownerAccount == User.Identity.Name)
            {
                // Use a transaction to prevent the user from deleting their last login credential
                using (
                    var scope = new TransactionScope(TransactionScopeOption.Required,
                        new TransactionOptions {IsolationLevel = IsolationLevel.Serializable}))
                {
                    var hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
                    if (hasLocalAccount || OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name).Count > 1)
                    {
                        OAuthWebSecurity.DeleteAccount(provider, providerUserId);
                        scope.Complete();
                        message = ManageMessageId.RemoveLoginSuccess;
                    }
                }
            }

            return RedirectToAction("Manage", new {Message = message});
        }

        //
        // GET: /Account/Manage

        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess
                    ? "Your password has been changed."
                    : message == ManageMessageId.SetPasswordSuccess
                        ? "Your password has been set."
                        : message == ManageMessageId.RemoveLoginSuccess
                            ? "The external login was removed."
                            : "";
            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {
            var hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword,
                            model.NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        return RedirectToAction("Manage", new {Message = ManageMessageId.ChangePasswordSuccess});
                    }
                    ModelState.AddModelError("", Resources.Resources.AccountController_Manage_The_current_password_is_incorrect_or_the_new_password_is_invalid_);
                }
            }
            else
            {
                // User does not have a local password so remove any validation errors caused by a missing
                // OldPassword field
                var state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        return RedirectToAction("Manage", new {Message = ManageMessageId.SetPasswordSuccess});
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("",
                            String.Format(
                                "Unable to create local account. An account with the name \"{0}\" may already exist.",
                                User.Identity.Name));
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new {ReturnUrl = returnUrl}));
        }

        //
        // GET: /Account/ExternalLoginCallback

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            var result =
                OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new {ReturnUrl = returnUrl}));
            if (!result.IsSuccessful)
            {
                return RedirectToAction("ExternalLoginFailure");
            }

            if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, false))
            {
                return RedirectToLocal(returnUrl);
            }

            if (User.Identity.IsAuthenticated)
            {
                // If the current user is logged in add the new account
                OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, User.Identity.Name);
                return RedirectToLocal(returnUrl);
            }
            // User is new, ask for their desired membership name
            var loginData = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
            ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName;
            ViewBag.ReturnUrl = returnUrl;
            return View("ExternalLoginConfirmation",
                new RegisterExternalLoginModel {UserName = result.UserName, ExternalLoginData = loginData});
        }

        //
        // POST: /Account/ExternalLoginConfirmation

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLoginConfirmation(RegisterExternalLoginModel model, string returnUrl)
        {
            string provider;
            string providerUserId;

            if (User.Identity.IsAuthenticated ||
                !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId))
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Insert a new user into the database
                using (var db = new UsersContext())
                {
                    var user =
                        db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());
                    // Check if user already exists
                    if (user == null)
                    {
                        // Insert name into the profile table
                        db.UserProfiles.Add(new UserProfile {UserName = model.UserName});
                        db.SaveChanges();

                        OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, model.UserName);
                        OAuthWebSecurity.Login(provider, providerUserId, false);

                        return RedirectToLocal(returnUrl);
                    }
                    ModelState.AddModelError("UserName", "User name already exists. Please enter a different user name.");
                }
            }

            ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(provider).DisplayName;
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // GET: /Account/ExternalLoginFailure

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult ExternalLoginsList(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_ExternalLoginsListPartial", OAuthWebSecurity.RegisteredClientData);
        }

        [ChildActionOnly]
        public ActionResult RemoveExternalLogins()
        {
            var accounts = OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name);
            var externalLogins = new List<ExternalLogin>();
            foreach (var account in accounts)
            {
                var clientData = OAuthWebSecurity.GetOAuthClientData(account.Provider);

                externalLogins.Add(new ExternalLogin
                {
                    Provider = account.Provider,
                    ProviderDisplayName = clientData.DisplayName,
                    ProviderUserId = account.ProviderUserId,
                });
            }

            ViewBag.ShowRemoveButton = externalLogins.Count > 1 ||
                                       OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            return PartialView("_RemoveExternalLoginsPartial", externalLogins);
        }

        [AllowAnonymous]
        public ActionResult Activate(string id)
        {
            var guid = new Guid(id);
            var activate = _emailActivationRepository.FindBy(ea => ea.Code == guid);
            if (!activate.Activated)
            {
                activate.Activated = true;
            }
            var user = _userRepository.FindBy(u => u.Account.Email == activate.Email);
            user.Activated = true;

            _emailActivationRepository.Update(activate);
            _userRepository.Update(user);
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public PartialViewResult LoginPartial()
        {
            return PartialView();
        }

        [AllowAnonymous]
        public PartialViewResult RegisterPartial()
        {
            return PartialView();
        }

        #region Helpers

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return
                        "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return
                        "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return
                        "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return
                        "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        #endregion
    }
}