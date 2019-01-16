using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using AutoMapper;
using WAVE.Dal.Entities;
using WAVE.Dal.Infrastructure;
using WAVE.Website.Classes;
using WAVE.Website.Models;
using Action = WAVE.Dal.Entities.Action;
using Filter = WAVE.Website.Models.Filter;

namespace WAVE.Website.Controllers
{
    public class EventsController : CultureController
    {
        private readonly IRepository<Action> _actionRepository;
        private readonly IRepository<ActionUpdate> _actionUpdateRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<ImageData> _imageDataRepository;
        private readonly IRepository<User> _userRepository;

        public EventsController(IRepository<Action> repo, IRepository<User> repo2, IRepository<Category> repo3,
            IRepository<ActionUpdate> repo4, IRepository<Comment> repo5, IRepository<ImageData> repo6)
        {
            _actionRepository = repo;
            _userRepository = repo2;
            _categoryRepository = repo3;
            _actionUpdateRepository = repo4;
            _commentRepository = repo5;
            _imageDataRepository = repo6;
        }


        public ActionResult Reset()
        {
            Session["Category"] = null;
            Session["Sort"] = null;
            Session["UserType"] = null;
            Session["Status"] = null;
            return RedirectToAction("Index");
        }

        //
        // GET: /Actions/Category/Environment

        public ActionResult Category(string id)
        {
            Session["Category"] = id;
            return RedirectToAction("Index");
        }

        public ActionResult Sort(string id)
        {
            Session["Sort"] = id;
            return RedirectToAction("Index");
        }

        public ActionResult UserType(string id)
        {
            Session["UserType"] = id;
            return RedirectToAction("Index");
        }

        public ActionResult Status(string id)
        {
            Session["Status"] = id;
            return RedirectToAction("Index");
        }

        //
        // GET: /Actions/

        public ActionResult Index()
        {
            var model = Mapper.Map<EventsIndexModel>(BaseModel);
            model.Modals.Add(new Modal()
            {
                Path = "CreateEvent.cshtml",
                Id = "modal-1",
                Width = "850px",
                Model = new EventsCreateModel()
            });
            //Initialize Session
            var categories = _categoryRepository.All().ToList();

            foreach (var cat in categories)
            {
                model.CategoryList.Add(new CategoryItems(cat, true));
            }
            model.Sort = Models.Sort.Latest;

            model.OrganizedBy = OrganizedBy.Everybody;

            model.Filter = Filter.All;

            model.Status = Models.Status.All;

            model.Actions = _actionRepository.All().ToList();
            ViewBag.Ip = model.Ip;
            return View(model);
        }


        //
        // GET: /Actions/Details/5

        public ActionResult Details(int id)
        {
            var model = Mapper.Map<EventsDetailsModel>(BaseModel);
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var username = HttpContext.User.Identity.Name;
                ViewBag.CurrentUser = _userRepository.FindBy(u => u.Account.UserName == username);
            }
            model.Action = _actionRepository.FindBy(a => a.Id == id);


            model.Modals.Add(new Modal()
            {
                Path = "EditEvent.cshtml",
                Id = "modal-1",
                Width = "850px",
                Model = Transformer.GetModelFromEntity(model.Action)
            });

            model.Modals.Add(new Modal()
            {
                Path = "EventsUploadImage.cshtml",
                Id = "modalUpload",
                Width = "850px"
            });
            model.Title = model.Action.Title;

            return View(model);
        }

        //
        // POST: /Actions/Create
        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(EventsCreateModel model)
        {
            if (model.StartDate != null && model.EndDate != null)
            {
                var startDate = (DateTime) model.StartDate;
                var endDate = (DateTime) model.EndDate;

                var imgData = ImageUploader.SaveFile(model.Image, BaseModel.LoggedUser);
                var action = new Action
                {
                    Title = model.Title,
                    Adress = model.Address,
                    Location = model.Country,
                    Creator = BaseModel.LoggedUser,
                    BannerPhoto = _imageDataRepository.FindBy(i => i.Id == 3),
                    ThumbnailPhoto = imgData,
                    StartDate =
                        new DateTime(startDate.Year, startDate.Month, startDate.Day, model.StartTime.Hour,
                            model.StartTime.Minute, model.StartTime.Second),
                    EndDate =
                        new DateTime(endDate.Year, endDate.Month, endDate.Day, model.EndTime.Hour, model.EndTime.Minute,
                            model.EndTime.Second),
                    Description = model.Description,
                    CurrentValueBar = 0,
                    MaxValueBar = model.Goal,
                    Category = _categoryRepository.FindBy(c => c.Title == model.Category),
                    MinValueBar = 0,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Latitude = model.Latitude,
                    Longtitude = model.Longitude,
                    Goal1Max = model.Goal,
                    Goal1Description = "",
                    Goal2Max = 0,
                    Goal2Description = "",
                    Goal3Max = 0,
                    Goal3Description = "",
                    TwitterHashtag = ""
                };
                _actionRepository.Add(action);
                var aId = _actionRepository.FindBy(a => a == action).Id;
                return RedirectToAction("Details", new {id = aId});
            }
            return null;
        }


        //
        // POST: /Actions/Edit
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
            var id = Int32.Parse(collection["Id"]);
            var startDate = DateTime.Parse(collection["StartDate"]);
            var startTime = DateTime.Parse(collection["StartTime"]);
            var newStartTime = new DateTime(startDate.Year, startDate.Month, startDate.Day, startTime.Hour,
                startTime.Minute, startTime.Second);
            var endDate = DateTime.Parse(collection["EndDate"]);
            var endTime = DateTime.Parse(collection["EndTime"]);
            var newEndTime = new DateTime(endDate.Year, endDate.Month, endDate.Day, endTime.Hour, endTime.Minute,
                endTime.Second);

            var action = _actionRepository.FindBy(a => a.Id == id);
            var usr = _userRepository.FindBy(u => u.Account.UserName == username);


            if (action.Creator == usr)
            {
                action.Title = collection["Title"];
                action.Adress = collection["Address"];
                action.Location = collection["Country"];
                action.StartDate = newStartTime;
                action.EndDate = newEndTime;
                action.Description = collection["Description"];
                action.MaxValueBar = Int32.Parse(collection["Volunteers"]);
                action.Category = _categoryRepository.FindBy(c => c.Title == collection["Category"]);
                action.MinValueBar = 0;
                action.DateModified = DateTime.Now;
                action.Latitude = 0;
                action.Longtitude = 0;
                action.Goal1Max = Int32.Parse(collection["Volunteers"]);
                action.Goal1Description = "";
                action.Goal2Max = 0;
                action.Goal2Description = "";
                action.Goal3Max = 0;
                action.Goal3Description = "";
                action.TwitterHashtag = "";
                _actionRepository.Update(action);
                var aId = _actionRepository.FindBy(a => a == action).Id;
                return RedirectToAction("Details", new { id = aId });
            }
            throw new UnauthorizedAccessException();

        }

        //
        // GET: /Actions/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            var username = "";
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                username = HttpContext.User.Identity.Name;
            }

            var action = _actionRepository.FindBy(a => a.Id == id);
            var usr = _userRepository.FindBy(u => u.Account.UserName == username);
            if (action.Creator == usr)
            {
                _actionRepository.Delete(action);
                return RedirectToAction("Index");
            }
            throw new UnauthorizedAccessException();
        }

        //
        // POST: /Actions/CreateUpdate
        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateUpdate(FormCollection collection)
        {
            var username = "";
            var id = Int32.Parse(collection["Id"]);
            try
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    username = HttpContext.User.Identity.Name;
                }
                var user = _userRepository.FindBy(u => u.Account.UserName == username);
                var action = _actionRepository.FindBy(a => a.Id == id);

                if (action.Creator == user)
                {
                    var actionUpdate = new ActionUpdate
                    {
                        Action = action,
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now,
                        Text = collection["UpdateText"]
                    };
                    _actionUpdateRepository.Add(actionUpdate);
                }
                return RedirectToAction("Details", new { id });
            }
            catch (Exception)
            {
                return RedirectToAction("Details", new { id });
            }
        }


        //
        // POST: /Actions/EditUpdate
        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditUpdate(FormCollection collection)
        {
            var username = "";
            var id = Int32.Parse(collection["Id"]);
            var aid = Int32.Parse(collection["ActionUpdateId"]);
            try
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    username = HttpContext.User.Identity.Name;
                }
                var user = _userRepository.FindBy(u => u.Account.UserName == username);
                var action = _actionRepository.FindBy(a => a.Id == id);

                if (action.Creator == user)
                {
                    var actionUpdate = _actionUpdateRepository.FindBy(au => au.Id == aid);
                    actionUpdate.Text = collection["UpdateText"];
                    actionUpdate.DateModified = DateTime.Now;

                    _actionUpdateRepository.Update(actionUpdate);
                }
                return RedirectToAction("Details", new { id });
            }
            catch (Exception)
            {
                return RedirectToAction("Details", new { id });
            }
        }

        //
        // POST: /Actions/DeleteUpdate
        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DeleteUpdate(FormCollection collection)
        {
            var username = "";
            var id = Int32.Parse(collection["Id"]);
            var aid = Int32.Parse(collection["ActionUpdateId"]);
            try
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    username = HttpContext.User.Identity.Name;
                }
                var user = _userRepository.FindBy(u => u.Account.UserName == username);
                var action = _actionRepository.FindBy(a => a.Id == id);

                if (action.Creator == user)
                {
                    var actionUpdate = _actionUpdateRepository.FindBy(au => au.Id == aid);

                    _actionUpdateRepository.Delete(actionUpdate);
                }
                return RedirectToAction("Details", new { id });
            }
            catch (Exception)
            {
                return RedirectToAction("Details", new { id });
            }
        }

        //
        // POST: /Actions/CreateComment
        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateComment(FormCollection collection)
        {
            var username = "";
            var id = Int32.Parse(collection["Id"]);
            try
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    username = HttpContext.User.Identity.Name;
                }
                var user = _userRepository.FindBy(u => u.Account.UserName == username);
                var action = _actionRepository.FindBy(a => a.Id == id);


                var comment = new Comment
                {
                    Action = action,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Text = collection["CommentText"],
                    User = user
                };
                _commentRepository.Add(comment);

                return RedirectToAction("Details", new { id });
            }
            catch (Exception)
            {
                return RedirectToAction("Details", new { id });
            }
        }


        //
        // POST: /Actions/EditComment

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditComment(FormCollection collection)
        {
            var username = "";
            var id = Int32.Parse(collection["Id"]);
            var cid = Int32.Parse(collection["CommentId"]);
            try
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    username = HttpContext.User.Identity.Name;
                }
                var user = _userRepository.FindBy(u => u.Account.UserName == username);


                var comment = _commentRepository.FindBy(c => c.Id == cid);
                if (comment.User == user || HttpContext.User.IsInRole("SuperAdministrator"))
                {
                    comment.Text = collection["UpdateText"];
                    comment.DateModified = DateTime.Now;
                }
                _commentRepository.Update(comment);

                return RedirectToAction("Details", new { id });
            }
            catch (Exception)
            {
                return RedirectToAction("Details", new { id });
            }
        }

        //
        // POST: /Actions/DeleteComment
        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DeleteComment(FormCollection collection)
        {
            var id = Int32.Parse(collection["Id"]);
            var cid = Int32.Parse(collection["CommentId"]);
            try
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                }

                if (HttpContext.User.IsInRole("SuperAdministrator"))
                {
                    var comment = _commentRepository.FindBy(c => c.Id == cid);
                    _commentRepository.Delete(comment);
                }
                return RedirectToAction("Details", new { id });
            }
            catch (Exception)
            {
                return RedirectToAction("Details", new { id });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult UploadFileThumb(int thumpActionId, IEnumerable<HttpPostedFileBase> files)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    var imgData = ImageUploader.SaveFile(file, BaseModel.LoggedUser);
                    if (imgData != null)
                    {
                        var action = _actionRepository.FindBy(a => a.Id == thumpActionId);
                        action.ThumbnailPhoto = imgData;
                        _actionRepository.Update(action);
                    }
                }
            }

            return RedirectToAction("Details", new { id = thumpActionId });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadFileBanner(int bannerActionId, IEnumerable<HttpPostedFileBase> files)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    var imgData = ImageUploader.SaveFile(file, BaseModel.LoggedUser);
                    if (imgData != null)
                    {
                        var action = _actionRepository.FindBy(a => a.Id == bannerActionId);
                        action.BannerPhoto = imgData;
                        _actionRepository.Update(action);
                    }
                }
            }

            return RedirectToAction("Details", new { id = bannerActionId });
        }

        [Authorize]
        public ActionResult Start()
        {
            return RedirectToLocal("/Events/#Add");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            //if (Url.IsLocalUrl(returnUrl))
            //{
            return Redirect(returnUrl);
            //}
            //return RedirectToAction("Index", "Home");
        }
    }
}