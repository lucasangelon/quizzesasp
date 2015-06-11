using Quizzes7.DAL;
using Quizzes7.Models;
using Quizzes7.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Quizzes7.Controllers
{
    public class QuestionController : Controller
    {
        QuizzesContext databaseContext = new QuizzesContext();

        // GET: Question
        public ActionResult Index()
        {
            var questions = databaseContext.question
                .Include(q => q.type)
                .Include(q => q.language)
                .Include(q => q.specific)
                .Include(q => q.quiz)
                .Include(q => q.question_extras)
                .OrderBy(q => q.quiz_id)
                .ToList();

            return View(questions);
        }

        public ActionResult Create()
        {
            populateQuizDropdown();
            populateLanguageDropdown();
            populateSpecificDropdown();
            populateTypeDropdown();
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "content, quiz_id, type_id, language_id, specific_id, correct_answer")]Question question)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    databaseContext.question.Add(question);
                    databaseContext.SaveChanges();
                    if (question.type_id == 1)
                    {
                        return RedirectToAction("MultipleChoice");
                    }
                    
                    // True or False = 2, Blank Space = 3
                    else if (question.type_id == 2 || question.type_id == 3)
                    {
                        return RedirectToAction("Index");
                    }

                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            populateLanguageDropdown(question.language_id);
            populateSpecificDropdown(question.specific_id);
            populateTypeDropdown(question.type_id);
            populateQuizDropdown(question.quiz_id);
            return View(question);
        }

        public ActionResult MultipleChoice(int? id)
        {
            ViewBag.QuestionID = id;
            return View();
        }

        [HttpPost]
        public ActionResult MultipleChoice([Bind(Include = "content, question_id")]QuestionExtra questionExtra, 
                                            [Bind(Include = "content, question_id")]QuestionExtra questionExtra2, 
                                            [Bind(Include = "content, question_id")]QuestionExtra questionExtra3, 
                                            [Bind(Include = "content, question_id")]QuestionExtra questionExtra4)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    databaseContext.question_extra.Add(questionExtra);
                    databaseContext.question_extra.Add(questionExtra2);
                    databaseContext.question_extra.Add(questionExtra3);
                    databaseContext.question_extra.Add(questionExtra4);

                    databaseContext.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Question question = databaseContext.question.Find(id);

            if (question == null)
            {
                return HttpNotFound();
            }

            populateLanguageDropdown(question.language_id);
            populateSpecificDropdown(question.specific_id);
            populateTypeDropdown(question.type_id);
            populateQuizDropdown(question.quiz_id);
            return View(question);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var questionToUpdate = databaseContext.question.Find(id);

            if (TryUpdateModel(questionToUpdate, "", new string[] { "content", "quiz_id", "type_id", "language_id", "specific_id", "correct_answer"}))
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

            populateLanguageDropdown(questionToUpdate.language_id);
            populateSpecificDropdown(questionToUpdate.specific_id);
            populateTypeDropdown(questionToUpdate.type_id);
            populateQuizDropdown(questionToUpdate.quiz_id);
            return View(questionToUpdate);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Question question = databaseContext.question.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = databaseContext.question
              .Where(i => i.id == id)
              .Single();

            databaseContext.question.Remove(question);
            databaseContext.SaveChanges();

            return RedirectToAction("Index");
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

        private void populateTypeDropdown(object selectedType = null)
        {
            var typesQuery = from type in databaseContext.type
                                 orderby type.name
                                 select type;
            ViewBag.TypeID = new SelectList(typesQuery, "id", "name", selectedType);
        }

        private void populateQuizDropdown(object selectedQuiz = null)
        {
            var quizzesQuery = from quiz in databaseContext.quiz
                             orderby quiz.title
                             select quiz;
            ViewBag.QuizID = new SelectList(quizzesQuery, "id", "title", selectedQuiz);
        }

        private QuestionExtra generateTrueFalse(string content, int question_id)
        {
            QuestionExtra qe = new QuestionExtra();
            qe.content = content;
            qe.question_id = question_id;

            return qe;
        }
    }
}