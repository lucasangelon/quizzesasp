using Quizzes7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzes7.ViewModels
{
    public class UserUnitIndexData
    {
        public IEnumerable<Unit> units { get; set; }
        public IEnumerable<User> lecturers { get; set; }
        public IEnumerable<User> students { get; set; }
    }
}
