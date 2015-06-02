using Quizzes7.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quizzes7.Controllers
{
    /// <summary>
    /// Responsible for rendering quick messages to the user.
    /// Retrieved from: http://www.talksharp.com/aspnet-mvc-how-to-display-a-message-after-calling-redirecttoaction
    /// Author: Tony Mackay
    /// </summary>
    public class MessageController : Controller
    {
        private QuizzesContext db = new QuizzesContext();
        /// <summary>
        /// Gets the partial view for the message.
        /// </summary>
        /// <returns>Message partial view.</returns>
        [ChildActionOnly]
        public ActionResult TemporaryMessage()
        {
            return PartialView();
        }
    }
}