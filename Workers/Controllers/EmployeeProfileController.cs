using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workers.Web.DataContext;
using Workers.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Workers.Web.Controllers
{
    public class EmployeeProfileController : Controller
    {
        private AppDbContext _context;

        public EmployeeProfileController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var EmployeeProfiles = _context.EmployeeProfiles.Include(e => e.Employee).Include(e => e.Profile).ToList();
            return View(EmployeeProfiles);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(EmployeeProfile employeeProfile)
        {
            if (ModelState.IsValid)
            {
                var newIdEmployee = _context.Employees.Select(x => x.id).Max() + 1;
                var newIdProfile = _context.Profiles.Select(x => x.id).Max() + 1;
                var newIdEmployeeProfile = _context.EmployeeProfiles.Select(x => x.id).Max() + 1;
                employeeProfile.id = newIdEmployeeProfile;

                var profile = new Profile()
                {
                    id = newIdProfile,
                    name = employeeProfile.Profile.name,
                    phone = employeeProfile.Profile.phone,
                    email = employeeProfile.Profile.email,
                    birthday = employeeProfile.Profile.birthday,
                    employeeId = newIdEmployee
                };
                _context.Profiles.Add(profile);
                _context.SaveChanges();

                var employee = new Employee()
                {
                    id = newIdEmployee,
                    post = employeeProfile.Employee.post,
                    devision = employeeProfile.Employee.devision,
                    salary = employeeProfile.Employee.salary,
                    profileId = newIdProfile
                };
                _context.Employees.Add(employee);
                _context.SaveChanges();

                employeeProfile.Employee = employee;
                employeeProfile.Profile = profile;

                _context.EmployeeProfiles.Add(employeeProfile);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _context.EmployeeProfiles.Include(e => e.Employee).Include(e => e.Profile).ToList();

            var employeeProfile = _context.EmployeeProfiles.Find(id);
            var empId = employeeProfile.Employee.id;
            var profId = employeeProfile.Profile.id;

            _context.EmployeeProfiles.Remove(employeeProfile);
            _context.Employees.Remove(_context.Employees.Find(empId));
            _context.Profiles.Remove(_context.Profiles.Find(profId));
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            _context.EmployeeProfiles.Include(e => e.Employee).Include(e => e.Profile).ToList();
            var employeeProfile = _context.EmployeeProfiles.Find(id);
            return View(employeeProfile);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeProfile employeeProfile)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Update(employeeProfile.Employee);
                _context.SaveChanges();

                _context.Profiles.Update(employeeProfile.Profile);
                _context.SaveChanges();

                _context.EmployeeProfiles.Update(employeeProfile);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult IndexDivisionFilter(IEnumerable<EmployeeProfile> model)
        {
            string division = Request.Form["division"];
            var EmployeeProfiles = _context.EmployeeProfiles.Include(e => e.Employee).Include(e => e.Profile).ToList();

            if (division != null)
            {
                EmployeeProfiles = EmployeeProfiles.Where(x => x.Employee.devision.StartsWith(division)).ToList();
            }
            return View("Index", EmployeeProfiles);
        }
    }
}