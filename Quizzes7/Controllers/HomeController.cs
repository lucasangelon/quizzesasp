using Quizzes7.DAL;
using Quizzes7.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quizzes7.Controllers
{
    public class HomeController : Controller
    {
        private QuizzesContext databaseContext = new QuizzesContext();
        private LoginHelper loginHelper = new LoginHelper();
        private MessageHelper messageHelper = new MessageHelper();

        public ActionResult Index()
        {
            ViewBag.Message = databaseContext.role.Find(1);
            return View(databaseContext.role.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// Homepage action, requires the user to be logged in.
        /// </summary>
        /// <returns>Homepage view.</returns>
        public ActionResult Homepage()
        {
            // Checking if the user is logged in.
            if (loginHelper.checkLogin((getCookieArray())[0], Session["AuthID"].ToString()))
            {
                return View();
            }
            else
            {
                TempData["Message"] = messageHelper.getMessage(1);
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Retrieves the cookie array containing details from the user.
        /// </summary>
        /// <returns>A string array.</returns>
        public string[] getCookieArray()
        {
            // Splitting the cookie into an array.
            string[] cookieArray = Request.Cookies["AuthID"].Value.ToString().Split(new string[] { " " }, StringSplitOptions.None);

            return cookieArray;
        }
    }
}