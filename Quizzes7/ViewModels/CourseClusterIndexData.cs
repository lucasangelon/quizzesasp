using Quizzes7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzes7.ViewModels
{
    public class CourseClusterIndexData
    {
        public IEnumerable<Course> courses { get; set; }
        public IEnumerable<Cluster> clusters { get; set; }
        public IEnumerable<Unit> units { get; set; }
    }
}
