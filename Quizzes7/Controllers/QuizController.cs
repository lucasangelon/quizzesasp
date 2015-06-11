using Quizzes7.DAL;
using Quizzes7.ViewModels;
using Quizzes7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Data.Entity;

namespace Quizzes7.Controllers
{
    public class QuizController : Controller
    {
        QuizzesContext databaseContext = new QuizzesContext();

        // GET: Quiz
        public ActionResult Index()
        {
            return View(databaseContext.quiz.ToList());
        }

        public ActionResult Create()
        {
            populateUnitDropdown();
            populateLanguageDropdown();
            populateSpecificDropdown();
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "title, due_date, unit_id, language_id, specific_id")]Quiz quiz)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    databaseContext.quiz.Add(quiz);
                    databaseContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            populateLanguageDropdown(quiz.language_id);
            populateSpecificDropdown(quiz.specific_id);
            populateUnitDropdown(quiz.unit_id);
            return View(quiz);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Quiz quiz = databaseContext.quiz.Find(id);

            if (quiz == null)
            {
                return HttpNotFound();
            }

            populateLanguageDropdown(quiz.language_id);
            populateSpecificDropdown(quiz.specific_id);
            populateUnitDropdown(quiz.unit_id);
            return View(quiz);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var quizToUpdate = databaseContext.quiz.Find(id);

            if (TryUpdateModel(quizToUpdate, "", new string[] { "title", "due_date" }))
            {
                try
                {
                    databaseContext.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            populateLanguageDropdown(quizToUpdate.language_id);
            populateSpecificDropdown(quizToUpdate.specific_id);
            populateUnitDropdown(quizToUpdate.unit_id);
            return View(quizToUpdate);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Quiz quiz = databaseContext.quiz.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            return View(quiz);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Quiz quiz = databaseContext.quiz
              .Where(i => i.id == id)
              .Single();

            databaseContext.quiz.Remove(quiz);
            databaseContext.SaveChanges();

            return RedirectToAction("Index");
        }

        private void populateUnitDropdown(object selectedUnit = null)
        {
            var unitsQuery = from unit in databaseContext.unit
                             orderby unit.name
                             select unit;
            ViewBag.UnitID = new SelectList(unitsQuery, "id", "name", selectedUnit);
        }

        private void populateLanguageDropdown(object selectedLanguage = null)
        {
            var languagesQuery = from language in databaseContext.language
                             orderby language.name
                             select language;

            ViewBag.LanguageID = new SelectList(languagesQuery, "id", "name", selectedLanguage);
        }

        private void populateSpecificDropdown(object selectedSpecific = null)
        {
            var specificsQuery = from specific in databaseContext.specific
                             orderby specific.name
                             select specific;
            ViewBag.SpecificID = new SelectList(specificsQuery, "id", "name", selectedSpecific);
        }
    }
}