using Quizzes7.DAL;
using Quizzes7.Models;
using Quizzes7.Helpers;
using Quizzes7.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity.Infrastructure;

namespace Quizzes7.Controllers
{
    public class UserUnitController : Controller
    {
        private QuizzesContext databaseContext = new QuizzesContext();

        // GET: UserUnit
        public ActionResult IndexLecturer(int? id)
        {
            var viewModel = new UserUnitIndexData();
            viewModel.lecturers = databaseContext.user
                .Include(x => x.units)
                .Where(x => x.role_id == 2);

            if (id != null)
            {
                ViewBag.LecturerID = id.Value;
                viewModel.units = viewModel.lecturers.Where(i => i.id == id.Value).Single().units;
            }

            return View(viewModel);
        }

        public ActionResult IndexStudent(int? id)
        {
            var viewModel = new UserUnitIndexData();
            viewModel.students = databaseContext.user
                .Include(x => x.units)
                .Where(x => x.role_id == 1);

            if (id != null)
            {
                ViewBag.StudentID = id.Value;
                viewModel.units = viewModel.students.Where(i => i.id == id.Value).Single().units;
            }

            return View(viewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = databaseContext.user
                .Include(i => i.units)
                .Where(i => i.id == id)
                .Single();

            populateAssignedUnitsData(user);
            
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(int? id, string[] selectedUnits)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userToUpdate = databaseContext.user
                .Include(i => i.units)
                .Where(i => i.id == id)
                .Single();

            if (TryUpdateModel(userToUpdate, "", new string[] { "name" }))
            {
                try
                {
                    updateUserUnits(selectedUnits, userToUpdate);

                    databaseContext.SaveChanges();

                    return RedirectToAction("IndexLecturer");
                }
                catch(RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            populateAssignedUnitsData(userToUpdate);
            return View(userToUpdate);
        }

        private void populateAssignedUnitsData(User user)
        {
            var allUnits = databaseContext.unit;
            var userUnits = new HashSet<int>(user.units.Select(c => c.id));
            var viewModel = new List<AssignedUnitsData>();

            foreach (var unit in allUnits)
            {
                viewModel.Add(new AssignedUnitsData
                    {
                        UnitID = unit.id,
                        Name = unit.name,
                        Assigned = userUnits.Contains(unit.id)
                    });
            }

            ViewBag.Units = viewModel;
        }

        private void updateUserUnits(string[] selectedUnits, User userToUpdate)
        {
            if (selectedUnits == null)
            {
                userToUpdate.units = new List<Unit>();
                return;
            }

            var selectedUnitsHS = new HashSet<string>(selectedUnits);
            var userUnits = new HashSet<int>(userToUpdate.units.Select(c => c.id));

            foreach (var unit in databaseContext.unit)
            {
                if (selectedUnitsHS.Contains(unit.id.ToString()))
                {
                    if (!userUnits.Contains(unit.id))
                    {
                        userToUpdate.units.Add(unit);
                    }
                }
                else
                {
                    if (userUnits.Contains(unit.id))
                    {
                        userToUpdate.units.Remove(unit);
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                databaseContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}