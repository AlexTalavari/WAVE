using WAVE.Dal.Entities;
using WAVE.Dal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WAVE.Dal.Infrastructure;

namespace WAVE.AdminWebsite.Controllers
{
    public class HomeController : Controller
    {

        private readonly IRepository<ImageData> _imageDataActivationRepository;

        public HomeController(IRepository<ImageData> repo)
        {
            _imageDataActivationRepository = repo;
        }
        
        
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ImageCounter() 
        {
          ViewBag.Count =  _imageDataActivationRepository.All().Count();
          return View();
        }
    }
}
