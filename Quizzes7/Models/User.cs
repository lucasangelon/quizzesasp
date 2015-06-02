using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzes7.Models
{
    public class User
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int role_id { get; set; }

        public virtual Role role { get; set; }

        public string fullName()
        {
            return first_name + " " + last_name;
        }
    }
}
