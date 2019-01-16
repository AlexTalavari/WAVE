using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using WAVE.Dal.Entities;
using WAVE.Dal.Infrastructure;
using WAVE.Website.Classes;
using WAVE.Website.Models;

namespace WAVE.Website.Controllers
{
    public class HomeController : CultureController
    {
        private readonly IRepository<Action> _actionRepository;
        private readonly IRepository<User> _userRepository;

        public HomeController(IRepository<Action> repo, IRepository<User> repo2)
        {
            _actionRepository = repo;
            _userRepository = repo2;
        }

        public ActionResult Index()
        {
            var model = Mapper.Map<HomeModel>(BaseModel);
            model.TopActions =
                _actionRepository.FilterBy(a => a.IsSuggested).OrderBy(a => a.DateModified).Take(6).ToList();
            model.TopUsers = _userRepository.All().OrderBy(u => u.DateModified).Take(6).ToList();
            return View(model);
        }

        public ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            RouteData.Values["culture"] = culture; // set culture
            return RedirectToAction("Index");
        }
    }
}