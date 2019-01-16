using WAVE.Dal.Entities;
using WAVE.Dal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Action = WAVE.Dal.Entities.Action;
using WAVE.Dal.Infrastructure;

namespace WAVE.AdminWebsite.Controllers
{
    public class UserController : Controller
    {
        private readonly IRepository<Action> _actionRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<ActionUpdate> _actionUpdateRepository;
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<ImageData> _imageDataActivationRepository;
        //
        // GET: /User/
        public UserController(IRepository<Action> repo, IRepository<User> repo2, IRepository<Category> repo3, IRepository<ActionUpdate> repo4, IRepository<Comment> repo5, IRepository<ImageData> repo6)
        {
            _actionRepository = repo;
            _userRepository = repo2;
            _categoryRepository = repo3;
            _actionUpdateRepository = repo4;
            _commentRepository = repo5;
            _imageDataActivationRepository = repo6;
        }


        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ban(FormCollection collection)
        {
            var username = collection["UserName"];
            Roles.RemoveUserFromRole(username, "User");
            Roles.AddUserToRole(username, "Banned");
            return View("Index");
        }
    }
}
