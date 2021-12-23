using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workers.Web.DataContext;
using Workers.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Workers.Web.Controllers
{
    public class ProfileController : Controller
    {
        private AppDbContext _context;

        public ProfileController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var Profiles = _context.Profiles.ToList();
            return View(Profiles);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Profile profile)
        {
            if (ModelState.IsValid)
            {
                var newId = _context.Profiles.Select(x => x.id).Max() + 1;
                profile.id = newId;

                _context.Profiles.Add(profile);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _context.Profiles.Remove(_context.Profiles.Find(id));
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {

            var Profile = _context.Profiles.Find(id);
            return View(Profile);
        }

        [HttpPost]
        public IActionResult Edit(Profile Profile)
        {
            if (ModelState.IsValid)
            {
                _context.Profiles.Update(Profile);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }
    }
}