using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzes7.Models
{
    public class Quiz
    {
        public int id { get; set; }
        public string title { get; set; }
        public string due_date { get; set; }
        public int user_id { get; set; }
        public int unit_id { get; set; }
        public int language_id { get; set; }
        public int specific_id { get; set; }

    }
}
