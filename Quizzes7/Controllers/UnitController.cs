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
    public class UnitController : Controller
    {
        private QuizzesContext databaseContext = new QuizzesContext();
        private LoginHelper loginHelper = new LoginHelper();
        private MessageHelper messageHelper = new MessageHelper();

        // GET: Unit
        public ActionResult Index()
        {
            if (Session["AuthId"] != null)
            {
                if (loginHelper.checkLogin((getCookieArray())[0], Session["AuthId"].ToString()) & loginHelper.checkAccount((getCookieArray())[1]))
                {
                    return View(databaseContext.unit.ToList());
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

        // GET: Unit/Details/5
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
                    Unit unit = databaseContext.unit.Find(id);
                    if (unit == null)
                    {
                        return HttpNotFound();
                    }
                    return View(unit);
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

        // GET: Unit/Create
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

        // POST: Unit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] Unit unit)
        {
            if (Session["AuthId"] != null)
            {
                if (loginHelper.checkLogin((getCookieArray())[0], Session["AuthId"].ToString()) & loginHelper.checkAccount((getCookieArray())[1]))
                {
                    if (ModelState.IsValid)
                    {
                        databaseContext.unit.Add(unit);
                        databaseContext.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    return View(unit);
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

        // GET: Unit/Edit/5
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
                    Unit unit = databaseContext.unit.Find(id);
                    if (unit == null)
                    {
                        return HttpNotFound();
                    }
                    return View(unit);
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

        // POST: Unit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] Unit unit)
        {
            if (Session["AuthId"] != null)
            {
                if (loginHelper.checkLogin((getCookieArray())[0], Session["AuthId"].ToString()) & loginHelper.checkAccount((getCookieArray())[1]))
                {
                    if (ModelState.IsValid)
                    {
                        databaseContext.Entry(unit).State = EntityState.Modified;
                        databaseContext.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(unit);
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

        // GET: Unit/Delete/5
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
                    Unit unit = databaseContext.unit.Find(id);
                    if (unit == null)
                    {
                        return HttpNotFound();
                    }
                    return View(unit);
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

        // POST: Unit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["AuthId"] != null)
            {
                if (loginHelper.checkLogin((getCookieArray())[0], Session["AuthId"].ToString()) & loginHelper.checkAccount((getCookieArray())[1]))
                {
                    Unit unit = databaseContext.unit.Find(id);
                    databaseContext.unit.Remove(unit);
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