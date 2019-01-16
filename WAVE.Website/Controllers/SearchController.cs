using System.Linq;
using System.Web.Mvc;
using WAVE.Dal.Entities;
using WAVE.Dal.Infrastructure;

namespace WAVE.Website.Controllers
{
    public class SearchController : CultureController
    {
        private readonly IRepository<Action> _actionRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<User> _userRepository;

        public SearchController(IRepository<Action> repo, IRepository<User> repo2, IRepository<Category> repo3)
        {
            _actionRepository = repo;
            _userRepository = repo2;
            _categoryRepository = repo3;
        }

        //
        // GET: /Search/

        public ActionResult Index(FormCollection collection)
        {
            var st = collection["SearchTerm"];
            ViewBag.SearchTerm = st;
            var actions =
                _actionRepository.FilterBy(a => a.Description.Contains(st) || a.Title.Contains(st));
            return View(actions.ToList());
        }

        //
        // GET: /Search/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Search/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Search/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Search/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Search/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Search/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Search/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}