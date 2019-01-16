using System;
using System.Web.Mvc;
using WAVE.Dal.Entities;
using WAVE.Dal.Infrastructure;
using Action = WAVE.Dal.Entities.Action;

namespace WAVE.Website.Controllers
{
    public class VolunteerController : CultureController
    {
        private readonly IRepository<Action> _actionRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Volunteer> _volunteerRepository;

        public VolunteerController(IRepository<Action> repo, IRepository<User> repo2, IRepository<Volunteer> repo3)
        {
            _actionRepository = repo;
            _userRepository = repo2;
            _volunteerRepository = repo3;
        }


        //
        // POST: /Volunteer/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var source = Int32.Parse(collection["Id"]);

            string username;

            username = HttpContext.User.Identity.Name;
            var user = _userRepository.FindBy(u => u.Account.UserName == username);
            var action = _actionRepository.FindBy(a => a.Id == source);
            var vol = new Volunteer
            {
                Action = action,
                User = user,
                Type = VolunteerType.Option1,
                StartDate = DateTime.Now
            };
            _volunteerRepository.Add(vol);
            return RedirectToAction("Details", "Events", new { id = source });

        }

        //
        // GET: /Volunteer/Edit/5

        // POST: /Volunteer/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {

            // TODO: Add update logic here

            return RedirectToAction("Index", "User");

        }

        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            var source = Int32.Parse(collection["Id"]);

            string username = HttpContext.User.Identity.Name;
            var user = _userRepository.FindBy(u => u.Account.UserName == username);
            var action = _actionRepository.FindBy(a => a.Id == source);
            var vol = _volunteerRepository.FindBy(v => v.Action == action && v.User == user);
            _volunteerRepository.Delete(vol);
            return RedirectToAction("Details", "Events", new { id = source });

        }
    }
}