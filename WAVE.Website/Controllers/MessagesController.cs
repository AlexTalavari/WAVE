using System;
using System.Web.Mvc;
using AutoMapper;
using WAVE.Dal.Entities;
using WAVE.Dal.Infrastructure;
using WAVE.Website.Models;
using Action = WAVE.Dal.Entities.Action;

namespace WAVE.Website.Controllers
{
    public class MessagesController : CultureController
    {
        //
        // GET: /Messages/
        private readonly IRepository<Action> _actionRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<ImageData> _imageDataRepository;
        private readonly IRepository<Unite> _uniteRepository;
        private readonly IRepository<UserMessage> _userMessageRepository;
        private readonly IRepository<User> _userRepository;

        public MessagesController(IRepository<Action> repo, IRepository<User> repo2, IRepository<Category> repo3,
            IRepository<Unite> repo4, IRepository<ImageData> repo5, IRepository<UserMessage> repo6)
        {
            _actionRepository = repo;
            _userRepository = repo2;
            _categoryRepository = repo3;
            _uniteRepository = repo4;
            _imageDataRepository = repo5;
            _userMessageRepository = repo6;
        }

        [Authorize]
        public ActionResult Index()
        {
            var model = Mapper.Map<MessagesModel>(BaseModel);
            model.Init(BaseModel.LoggedUser, null);
            model.Modals.Add(new Modal()
            {
                Path = "ComposeMessage.cshtml",
                Id="modal-1",
                Width = "475px"
            });
            return View(model);
        }

        [Authorize]
        public ActionResult Details(string username)
        {
            var model = Mapper.Map<MessagesModel>(BaseModel);
            var active = _userRepository.FindBy(u => u.Account.UserName == username);
            model.Init(BaseModel.LoggedUser, active);
            model.Modals.Add(new Modal()
            {
                Path = "ComposeMessage.cshtml",
                Id = "modal-1",
                Width = "475px"
            });
            return View("Index", model);
        }

        // POST: /Messages/Compose
        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Compose(FormCollection collection)
        {
            var username = "";
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                username = HttpContext.User.Identity.Name;
            }
                var text = collection["Message"];

                if (text == "" || text == null)
                {
                    return RedirectToAction("Index");
                }
                var from = _userRepository.FindBy(u => u.Account.UserName == username);
                var to = _userRepository.FindBy(u => u.Account.UserName == collection["To"]);


                if (from != null && to != null)
                {
                    var um = new UserMessage
                    {
                        From = from,
                        To = to,
                        Text = text
                    };
                    _userMessageRepository.Add(um);
                    return RedirectToAction("Details", new {username = to.Account.UserName});
                }
                return RedirectToAction("Index");
            
        }
    }
}