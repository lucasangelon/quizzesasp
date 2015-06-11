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
    public class FAQController : Controller
    {
        private QuizzesContext databaseContext = new QuizzesContext();
        private LoginHelper loginHelper = new LoginHelper();
        private MessageHelper messageHelper = new MessageHelper();

        // GET: FAQ
        public ActionResult Index()
        {
            if (Session["AuthId"] != null)
            {
                if (loginHelper.checkLogin((getCookieArray())[0], Session["AuthId"].ToString()) & loginHelper.checkAccount((getCookieArray())[1]))
                {
                    return View(databaseContext.faq.ToList());
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

        // GET: FAQ/Details/5
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
                    FAQ faq = databaseContext.faq.Find(id);
                    if (faq == null)
                    {
                        return HttpNotFound();
                    }
                    return View(faq);
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

        // GET: FAQ/Create
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

        // POST: FAQ/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "name")] FAQ faq)
        {
            if (Session["AuthId"] != null)
            {
                if (loginHelper.checkLogin((getCookieArray())[0], Session["AuthId"].ToString()) & loginHelper.checkAccount((getCookieArray())[1]))
                {
                    if (ModelState.IsValid)
                    {
                        databaseContext.faq.Add(faq);
                        databaseContext.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    return View(faq);
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

        // GET: FAQ/Edit/5
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
                    FAQ faq = databaseContext.faq.Find(id);
                    if (faq == null)
                    {
                        return HttpNotFound();
                    }
                    return View(faq);
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

        // POST: FAQ/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "name")] FAQ faq)
        {
            if (Session["AuthId"] != null)
            {
                if (loginHelper.checkLogin((getCookieArray())[0], Session["AuthId"].ToString()) & loginHelper.checkAccount((getCookieArray())[1]))
                {
                    if (ModelState.IsValid)
                    {
                        databaseContext.Entry(faq).State = EntityState.Modified;
                        databaseContext.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(faq);
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

        // GET: FAQ/Delete/5
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
                    FAQ faq = databaseContext.faq.Find(id);
                    if (faq == null)
                    {
                        return HttpNotFound();
                    }
                    return View(faq);
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

        // POST: FAQ/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["AuthId"] != null)
            {
                if (loginHelper.checkLogin((getCookieArray())[0], Session["AuthId"].ToString()) & loginHelper.checkAccount((getCookieArray())[1]))
                {
                    FAQ faq = databaseContext.faq.Find(id);
                    databaseContext.faq.Remove(faq);
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