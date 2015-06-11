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
    public class TypeController : Controller
    {
        private QuizzesContext databaseContext = new QuizzesContext();
        private LoginHelper loginHelper = new LoginHelper();
        private MessageHelper messageHelper = new MessageHelper();

        // GET: Type
        public ActionResult Index()
        {
            if (Session["AuthId"] != null)
            {
                if (loginHelper.checkLogin((getCookieArray())[0], Session["AuthId"].ToString()) & loginHelper.checkAccount((getCookieArray())[1]))
                {
                    return View(databaseContext.type.ToList());
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

        // GET: Type/Details/5
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
                    Quizzes7.Models.Type type = databaseContext.type.Find(id);
                    if (type == null)
                    {
                        return HttpNotFound();
                    }
                    return View(type);
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

        // GET: Type/Create
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

        // POST: Type/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "name")] Quizzes7.Models.Type type)
        {
            if (Session["AuthId"] != null)
            {
                if (loginHelper.checkLogin((getCookieArray())[0], Session["AuthId"].ToString()) & loginHelper.checkAccount((getCookieArray())[1]))
                {
                    if (ModelState.IsValid)
                    {
                        databaseContext.type.Add(type);
                        databaseContext.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    return View(type);
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

        // GET: Type/Edit/5
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
                    Quizzes7.Models.Type type = databaseContext.type.Find(id);
                    if (type == null)
                    {
                        return HttpNotFound();
                    }
                    return View(type);
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

        // POST: Type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "name")] Quizzes7.Models.Type type)
        {
            if (Session["AuthId"] != null)
            {
                if (loginHelper.checkLogin((getCookieArray())[0], Session["AuthId"].ToString()) & loginHelper.checkAccount((getCookieArray())[1]))
                {
                    if (ModelState.IsValid)
                    {
                        databaseContext.Entry(type).State = EntityState.Modified;
                        databaseContext.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(type);
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

        // GET: Type/Delete/5
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
                    Quizzes7.Models.Type type = databaseContext.type.Find(id);
                    if (type == null)
                    {
                        return HttpNotFound();
                    }
                    return View(type);
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

        // POST: Type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["AuthId"] != null)
            {
                if (loginHelper.checkLogin((getCookieArray())[0], Session["AuthId"].ToString()) & loginHelper.checkAccount((getCookieArray())[1]))
                {
                    Quizzes7.Models.Type type = databaseContext.type.Find(id);
                    databaseContext.type.Remove(type);
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