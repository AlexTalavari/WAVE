using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using WAVE.Dal.Entities;
using WAVE.Dal.Infrastructure;
using WAVE.Website.Classes;
using WAVE.Website.Models;

namespace WAVE.Website.Controllers
{
    public class UserController : CultureController
    {
        private readonly IRepository<ImageData> _imageDataRepository;
        private readonly IRepository<User> _userRepository;

        public UserController( IRepository<User> repo2,IRepository<ImageData> repo5)
        {
           
            _userRepository = repo2;
           
            _imageDataRepository = repo5;
        }


        public ActionResult Default()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", new { @userName = HttpContext.User.Identity.Name });
            }
            return RedirectToAction("Index", new { @userName = "Wave" });
        }


        //
        // GET: /Profile/Details/5
        public ActionResult Index(string userName)
        {
            User currentUser;
            //ViewBag.Title = ViewBag.CurrentUser.GetFullName();
            var model = Mapper.Map<UserModel>(BaseModel);

            if (string.IsNullOrEmpty(userName))
            {
                currentUser = _userRepository.FindBy(u => u.Account.UserName == "Wave");
            }
            else
            {

                currentUser = _userRepository.FindBy(a => a.Account.UserName == userName);

            }
            model.Init(currentUser);

            if (currentUser.Account.UserName == model.LoggedUser.Account.UserName)
            {
                model.Modals.Add(new Modal
                {
                    Path = "EditUser.cshtml",
                    Id = "modal-1",
                    Width = "850px",
                    Model =Transformer.GetModelFromEntity(model.LoggedUser)
                });
                model.Modals.Add(new Modal
                {
                    Path = "UserUploadBanner.cshtml",
                    Id = "modalUpload",
                    Width = "850px"
                });
                model.Modals.Add(new Modal
                {
                    Path = "UserUploadProfile.cshtml",
                    Id = "modalUploadProfile",
                    Width = "850px"
                });
            }


            return View(model);
        }

        //
        // GET: /Profile/Edit/5
       

        //
        // POST: /Profile/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection collection)
        {
            var username = "";
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                username = HttpContext.User.Identity.Name;
            }
                DateTime dateofbirth;
                DateTime.TryParse(collection["DateOfBirth"], out dateofbirth);

                var user = _userRepository.FindBy(u => u.Account.UserName == username);

                user.Name = collection["Name"];
                user.Surname = collection["Surname"];
                user.Adress = collection["Address"];
                user.City = collection["City"];
                user.Country = collection["Country"];
                user.DateOfBirth = dateofbirth;
                user.Description = collection["Description"];
                user.Facebook = collection["Facebook"];
                user.GooglePlus = collection["GooglePlus"];
                user.Linkedin = collection["Linkedin"];
                user.DateModified = DateTime.Now;
                user.Phone = collection["Phone"];
                user.Skype = collection["Skype"];
                user.Twitter = collection["Twitter"];
                user.Website = collection["Twitter"];
                user.Youtube = collection["Twitter"];
                user.ZipCode = collection["ZipCode"];
                _userRepository.Update(user);

                return RedirectToAction("Index", new { @username = user.Account.UserName });
            
        }


        //
        // POST: /Profile/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult UploadFileProfile(string profileActionId, IEnumerable<HttpPostedFileBase> files)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    var imgData = ImageUploader.SaveFile(file, BaseModel.LoggedUser);
                    if (imgData != null)
                    {
                        _imageDataRepository.Add(imgData);
                        BaseModel.LoggedUser.ProfilePhoto = imgData;
                        _userRepository.Update(BaseModel.LoggedUser);
                    }
                }
            }

            return RedirectToAction("Index", new { @username = profileActionId });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadFileBanner(string bannerActionId, IEnumerable<HttpPostedFileBase> files)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    var imgData = ImageUploader.SaveFile(file, BaseModel.LoggedUser);
                    if (imgData != null)
                    {
                        _imageDataRepository.Add(imgData);
                        BaseModel.LoggedUser.BannerPhoto = imgData;
                        _userRepository.Update(BaseModel.LoggedUser);
                    }
                }
            }

            return RedirectToAction("Index", new { username = bannerActionId });
        }
    }
}