using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzes7.Models
{
    public class Cluster
    {
        public int id { get; set; }
        public string name { get; set; }

        public virtual ICollection<Course> courses { get; set; }
        public virtual ICollection<Unit> units { get; set; }
    }
}
