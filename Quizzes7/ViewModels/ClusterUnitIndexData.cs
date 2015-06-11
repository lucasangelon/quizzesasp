using System;
using Quizzes7.ViewModels;
using Quizzes7.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzes7.ViewModels
{
    public class ClusterUnitIndexData
    {
        public IEnumerable<Cluster> clusters { get; set; }
        public IEnumerable<Unit> units { get; set; }
        public IEnumerable<User> lecturers { get; set; }
    }
}
