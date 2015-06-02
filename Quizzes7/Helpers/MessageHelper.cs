using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzes7.Helpers
{
    public class MessageHelper
    {
        /// <summary>
        /// Gets the quick message to be displayed based on the type.
        /// </summary>
        /// <param name="type">Type of message to be retrieved, refer to the documentation for all the types available.</param>
        public string getMessage(int type, string userLastName = "")
        {
            string message = "";

            switch (type)
            {
                case 1:
                    message = "A login is required to use this feature.";
                    break;
                case 2:
                    message = "Invalid credentials, please try again.";
                    break;
                case 3:
                    message = "Invalid access.";
                    break;
                case 4:
                    if (userLastName != "")
                    {
                        message = "Welcome, " + userLastName + "!";
                    }
                    else
                    {
                        message = "";
                    }

                    break;
                default:
                    break;
            }

            return message;
        }
    }
}
