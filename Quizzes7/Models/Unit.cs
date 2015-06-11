using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzes7.Models
{
    public class Unit
    {
        public int id { get; set; }
        public string name { get; set; }

        public virtual ICollection<Cluster> clusters { get; set; }
        public virtual ICollection<User> users { get; set; }
        public virtual ICollection<Quiz> quizzes { get; set; }
    }
}
