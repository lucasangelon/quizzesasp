using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzes7.Models
{
    public class User
    {
        [Display(Name = "ID")]
        public int id { get; set; }

        [Display(Name = "First Name")]
        public string first_name { get; set; }

        [Display(Name = "Last Name")]
        public string last_name { get; set; }

        [Display(Name = "Email")]
        public string email { get; set; }

        [Display(Name = "Password")]
        public string password { get; set; }

        [Display(Name = "Role ID")]
        public int role_id { get; set; }

        public virtual ICollection<Unit> units { get; set; }

        [Display(Name = "Full Name")]
        public string fullName
        {
            get
            {
                return first_name + " " + last_name;
            }
        }
    }
}
