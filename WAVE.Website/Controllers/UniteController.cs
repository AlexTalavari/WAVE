using System;
using System.Web;
using System.Web.Mvc;
using WAVE.Dal.Entities;
using WAVE.Dal.Infrastructure;

namespace WAVE.Website.Controllers
{
    public class UniteController : CultureController
    {
        private readonly IRepository<Unite> _uniteRepository;
        private readonly IRepository<User> _userRepository;

        public UniteController(IRepository<User> repo, IRepository<Unite> repo2)
        {
            _userRepository = repo;
            _uniteRepository = repo2;
        }

        //
        // POST: /Actions/Create
        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Follow(FormCollection collection)
        {
            var username = "";
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                username = HttpContext.User.Identity.Name;
            }
            try
            {
                var toName = collection["Username"];

                var unite = new Unite
                {
                    From = _userRepository.FindBy(u => u.Account.UserName == username),
                    To = _userRepository.FindBy(u => u.Account.UserName == toName),
                };


                _uniteRepository.Add(unite);
                return RedirectToAction("Index", "User", new {@userName = toName});
            }
            catch (Exception)
            {
                throw new HttpException(500, "Some description");
            }
        }


        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Unfollow(FormCollection collection)
        {
            var username = "";
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                username = HttpContext.User.Identity.Name;
            }
            try
            {
                var toName = collection["Username"];

                var from = _userRepository.FindBy(u => u.Account.UserName == username);
                var to = _userRepository.FindBy(u => u.Account.UserName == toName);
                var unite = _uniteRepository.FindBy(un => un.From == from && un.To == to);

                _uniteRepository.Delete(unite);
                return RedirectToAction("Index", "User", new {@userName = toName});
            }
            catch (Exception)
            {
                throw new HttpException(500, "Some description");
            }
        }
    }
}