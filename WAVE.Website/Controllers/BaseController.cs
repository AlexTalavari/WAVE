using System.Collections.Generic;
using System.Web.Mvc;
using FluentNHibernate.Utils;
using WAVE.Dal.Entities;
using WAVE.Dal.Infrastructure;
using WAVE.Dal.Repositories;
using WAVE.Website.Models;

namespace WAVE.Website.Controllers
{
#if Release
    [ETag]
    [CacheFilter]
    [CompressFilter]
#endif
    
    public class BaseController : Controller
    {
        private readonly IRepository<User> _userRepository;

        public BaseController()
        {
            _userRepository = new Repository<User>();
            BaseModel = new LayoutModel {Modals = new List<Modal>()};
        }

        public User LoggedUser { get; set; }
        public LayoutModel BaseModel { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext ctx)
        {
            base.OnActionExecuting(ctx);

            BaseModel.Ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(BaseModel.Ip))
                BaseModel.Ip = Request.ServerVariables["REMOTE_ADDR"];


            if (HttpContext.User.Identity.IsAuthenticated)
            {
                if (Session != null && Session["User"] != null)
                {
                    LoggedUser = (User) Session["User"];
                }
                var curUsername = HttpContext.User.Identity.Name;
                if (LoggedUser == null || LoggedUser.Account.UserName != curUsername)
                {
                    LoggedUser = _userRepository.FindBy(u => u.Account.UserName == curUsername);
                    if (Session != null) Session["User"] = _userRepository.FindByEager(LoggedUser.Id);
                }
                BaseModel.LoggedUser = LoggedUser;
                BaseModel.Authenticated = true;
            }
            else
            {
                LoggedUser = new User
                {
                    Account = new Account {UserName = "Guest"},
                };
                BaseModel.LoggedUser = LoggedUser;
            }
        }


        // GET: /Default1/
    }
}