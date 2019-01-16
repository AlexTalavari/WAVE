using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WAVE.Dal.Entities;
using WAVE.Dal.Infrastructure;

namespace WAVE.Website.Controllers
{
    public class AboutController : CultureController
    {
        private readonly IRepository<Team> _teamRepository;

        public AboutController(IRepository<Team> repo)
        {
            _teamRepository = repo;
        }

        //

        // GET: /About/


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TermsOfUse()
        {
            return View();
        }

        public ActionResult PrivacyPolicy()
        {
            return View();
        }

        public ActionResult FAQs()
        {
            return View();
        }

        public ActionResult OurSupporters()
        {
            return View();
        }

        public ActionResult Ambassadors()
        {
            return View();
        }

        public ActionResult TheTeam()
        {
            var team = _teamRepository.All().ToList();
            return View(team);
        }
    }
}