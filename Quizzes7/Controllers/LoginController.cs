using Quizzes7.DAL;
using Quizzes7.Helpers;
using Quizzes7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quizzes7.Controllers
{
    /// <summary>
    /// Handles the login page and the functionality related to it.
    /// </summary>
    public class LoginController : Controller
    {
        private QuizzesContext databaseContext = new QuizzesContext();
        private MessageHelper messageHelper = new MessageHelper();

        /// <summary>
        /// Gets the index view.
        /// </summary>
        /// <returns>Index View</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Handles the user login.
        /// Code retrieved and modified from: http://codereview.stackexchange.com/questions/51331/simple-authentication-in-asp-net-mvc-5 
        /// </summary>
        /// <param name="user">A User object to extract the login details.</param>
        /// <returns></returns>
        public ActionResult Homepage(User user)
        {
            bool validEmail = databaseContext.user.Any(x => x.email == user.email);

            if (!validEmail)
            {
                TempData["Message"] = messageHelper.getMessage(2);
                return RedirectToAction("Index");
            }

            string password = databaseContext.user
                .Where(x => x.email == user.email)
                .Select(x => x.password)
                .Single();

            if (password != user.password)
            {
                TempData["Message"] = messageHelper.getMessage(2);
                return RedirectToAction("Index");
            }

            int userId = databaseContext.user
                .Where(x => x.email == user.email)
                .Select(x => x.id)
                .Single();

            User currentUser = databaseContext.user.SqlQuery("SELECT * FROM user WHERE id = " + userId.ToString()).Single();

            // If the user is a student.
            if (currentUser.role_id != 2 & currentUser.role_id != 3)
            {
                TempData["Message"] = messageHelper.getMessage(3);
                return RedirectToAction("Index");
            }

            string authId = Guid.NewGuid().ToString();

            Session["AuthId"] = authId;

            // Create a cookie containing the Authorization ID, the role and the user id.
            var cookie = new HttpCookie("AuthId");
            cookie.Value = authId + " " + currentUser.role_id + " " + currentUser.id;
            Response.Cookies.Add(cookie);

            // Set a welcome message to the user.
            TempData["Message"] = messageHelper.getMessage(4, currentUser.fullName());

            return RedirectToAction("Homepage", "Home");
        }

        public ActionResult Logout()
        {
            if (Request.Cookies["AuthId"] != null)
            {
                var cookie = new HttpCookie("AuthId");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}