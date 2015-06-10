using Quizzes7.DAL;
using Quizzes7.Helpers;
using Quizzes7.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Quizzes7.Controllers
{
    public class UserController : Controller
    {
        private QuizzesContext databaseContext = new QuizzesContext();
        private LoginHelper loginHelper = new LoginHelper();
        private MessageHelper messageHelper = new MessageHelper();

        /// <summary>
        /// Index action, shows the table of users to administrators. Requires
        /// to be logged in as an administrator in order to be accessible.
        /// </summary>
        /// <returns>Users index view.</returns>
        public ActionResult Index()
        {
            if (Session["AuthId"] != null)
            {
                if (loginHelper.checkLogin((getCookieArray())[0], Session["AuthId"].ToString()) & loginHelper.checkAccount((getCookieArray())[1]))
                {
                    return View(databaseContext.user.OrderBy(u => u.role_id).ToList());
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

        /// <summary>
        /// Editing view for users. Used by administrators.
        /// </summary>
        /// <param name="id">The ID of the user currently being edited.</param>
        /// <returns>The editing view with the user's details.</returns>
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

                    User user = databaseContext.user.Find(id);
                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    return View(user);
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

        // GET: Users/Create
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

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,first_name,last_name,email,password,role_id")] User user)
        {
            if (Session["AuthId"] != null)
            {
                if (loginHelper.checkLogin((getCookieArray())[0], Session["AuthId"].ToString()) & loginHelper.checkAccount((getCookieArray())[1]))
                {
                    if (ModelState.IsValid)
                    {
                        databaseContext.user.Add(user);
                        databaseContext.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    return View(user);
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

        // GET: Users/Edit/5
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
                    User user = databaseContext.user.Find(id);
                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    return View(user);
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

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,first_name,last_name,email,password,role_id")] User user)
        {
            if (Session["AuthId"] != null)
            {
                if (loginHelper.checkLogin((getCookieArray())[0], Session["AuthId"].ToString()) & loginHelper.checkAccount((getCookieArray())[1]))
                {
                    if (ModelState.IsValid)
                    {
                        databaseContext.Entry(user).State = EntityState.Modified;
                        databaseContext.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(user);
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

        // GET: Users/Delete/5
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
                    User user = databaseContext.user.Find(id);
                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    return View(user);
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

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["AuthId"] != null)
            {
                if (loginHelper.checkLogin((getCookieArray())[0], Session["AuthId"].ToString()) & loginHelper.checkAccount((getCookieArray())[1]))
                {
                    User user = databaseContext.user.Find(id);
                    databaseContext.user.Remove(user);
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