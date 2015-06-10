using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzes7.Helpers
{
    public class LoginHelper
    {
        /// <summary>
        /// Checks if the user is logged in.
        /// </summary>
        /// <param name="hash">The hash to be checked.</param>
        /// <param name="authId">The current session hash to be checked.</param>
        /// <returns>Returns true if the user has been validated, false if not.</returns>
        public bool checkLogin(string hash, string authId)
        {
            // Checking for errors.
            try
            {
                // Login validation.
                if (hash.Equals(authId))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if the user is an administrator.
        /// </summary>
        /// <param name="role">The role to be checked.</param>
        /// <returns>Returns true if the account matches the type, false if a lecturer is detected.</returns>
        public bool checkAccount(string role)
        {
            // Checking for errors.
            try
            {
                // Account validation.
                if (role == "3")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Parses the user id from a string to an int.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <returns>The user id as an integer.</returns>
        public int getUserId(string id)
        {
            // Checking for errors.
            try
            {
                return int.Parse(id.ToString());
            }
            catch
            {
                return 0;
            }
        }
    }
}
