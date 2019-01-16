using System.Web.Mvc;

namespace WAVE.Website.Controllers
{
    public class ErrorsController : CultureController
    {
        //
        // GET: /Errors/
        public ActionResult Index()
        {
            BaseModel.Title = "An error happened";
            return View(BaseModel);
        }

        public ActionResult NotFound()
        {
            BaseModel.Title = "Page not found";
            return View(BaseModel);
        }
    }
}