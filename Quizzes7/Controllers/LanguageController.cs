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

namespace Quizzes7.Controllers
{
    public class LanguageController : Controller
    {
        private QuizzesContext databaseContext = new QuizzesContext();
        private LoginHelper loginHelper = new LoginHelper();
        private MessageHelper messageHelper = new MessageHelper();

        // GET: Language
        public ActionResult Index()
        {
            if (Session["AuthId"] != null)
            {
                if (loginHelper.checkLogin((getCookieArray())[0], Session["AuthId"].ToString()) & loginHelper.checkAccount((getCookieArray())[1]))
                {
                    return View(databaseContext.language.ToList());
                }
                else
                {
                    TempData["Message"] = messageHelper.getMessage(3);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["Message"] = messageHelper.getMessage(1);
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Language/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["AuthId"] != null)
            {
                if (loginHelper.checkLogin((getCookieArray())[0], Session["AuthId"].ToString()) & loginHelper.checkAccount((getCookieArray())[1]))
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Language language = databaseContext.language.Find(id);
                    if (language == null)
                    {
                        return HttpNotFound();
                    }
                    return View(language);
                }
                else
                {
                    TempData["Message"] = messageHelper.getMessage(3);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["Message"] = messageHelper.getMessage(1);
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Language/Create
        public ActionResult Create()
        {
            if (Session["AuthId"] != null)
            {
                if (loginHelper.checkLogin((getCookieArray())[0], Session["AuthId"].ToString()) & loginHelper.checkAccount((getCookieArray())[1]))
                {
                    return View();
                }
                else
                {
                    TempData["Message"] = messageHelper.getMessage(3);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["Message"] = messageHelper.getMessage(1);
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Language/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "name")] Language language)
        {
            if (Session["AuthId"] != null)
            {
                if (loginHelper.checkLogin((getCookieArray())[0], Session["AuthId"].ToString()) & loginHelper.checkAccount((getCookieArray())[1]))
                {
                    if (ModelState.IsValid)
                    {
                        databaseContext.language.Add(language);
                        databaseContext.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    return View(language);
                }
                else
                {
                    TempData["Message"] = messageHelper.getMessage(3);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["Message"] = messageHelper.getMessage(1);
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Language/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["AuthId"] != null)
            {
                if (loginHelper.checkLogin((getCookieArray())[0], Session["AuthId"].ToString()) & loginHelper.checkAccount((getCookieArray())[1]))
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Language language = databaseContext.language.Find(id);
                    if (language == null)
                    {
                        return HttpNotFound();
                    }
                    return View(language);
                }
                else
                {
                    TempData["Message"] = messageHelper.getMessage(3);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["Message"] = messageHelper.getMessage(1);
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Language/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "name")] Language language)
        {
            if (Session["AuthId"] != null)
            {
                if (loginHelper.checkLogin((getCookieArray())[0], Session["AuthId"].ToString()) & loginHelper.checkAccount((getCookieArray())[1]))
                {
                    if (ModelState.IsValid)
                    {
                        databaseContext.Entry(language).State = EntityState.Modified;
                        databaseContext.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(language);
                }
                else
                {
                    TempData["Message"] = messageHelper.getMessage(3);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["Message"] = messageHelper.getMessage(1);
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Language/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["AuthId"] != null)
            {
                if (loginHelper.checkLogin((getCookieArray())[0], Session["AuthId"].ToString()) & loginHelper.checkAccount((getCookieArray())[1]))
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Language language = databaseContext.language.Find(id);
                    if (language == null)
                    {
                        return HttpNotFound();
                    }
                    return View(language);
                }
                else
                {
                    TempData["Message"] = messageHelper.getMessage(3);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["Message"] = messageHelper.getMessage(1);
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Language/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["AuthId"] != null)
            {
                if (loginHelper.checkLogin((getCookieArray())[0], Session["AuthId"].ToString()) & loginHelper.checkAccount((getCookieArray())[1]))
                {
                    Language language = databaseContext.language.Find(id);
                    databaseContext.language.Remove(language);
                    databaseContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = messageHelper.getMessage(3);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["Message"] = messageHelper.getMessage(1);
                return RedirectToAction("Index", "Home");
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

        /// <summary>
        /// Retrieves the cookie array containing details from the user.
        /// </summary>
        /// <returns>A string array.</returns>
        public string[] getCookieArray()
        {
            // Splitting the cookie into an array.
            string[] cookieArray = Request.Cookies["AuthId"].Value.ToString().Split(new string[] { " " }, StringSplitOptions.None);

            return cookieArray;
        }
    }
}