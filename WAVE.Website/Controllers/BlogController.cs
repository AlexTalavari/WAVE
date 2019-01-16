using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WAVE.Dal.Entities;
using WAVE.Dal.Infrastructure;

namespace WAVE.Website.Controllers
{
    public class BlogController : CultureController
    {
        //
        // GET: /Blog/

        private readonly IRepository<BlogPost> _blogPostRepository;
        private readonly IRepository<User> _userRepository;

        public BlogController(IRepository<BlogPost> repo, IRepository<User> repo2)
        {
            _blogPostRepository = repo;
            _userRepository = repo2;
        }


        public ActionResult Index()
        {
            var blogposts = _blogPostRepository.All().ToList();
            return View(blogposts);
        }

        //
        // GET: /Blog/Details/5

        public ActionResult Details(int id)
        {
            var blogpost = _blogPostRepository.FindBy(b => b.Id == id);
            return View(blogpost);
        }

        //
        // GET: /Blog/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Blog/Create

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
        // GET: /Blog/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Blog/Edit/5

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
        // GET: /Blog/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Blog/Delete/5

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