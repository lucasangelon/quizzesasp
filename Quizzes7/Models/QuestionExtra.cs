using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzes7.Models
{
    public class QuestionExtra
    {
        public int id { get; set; }
        public string content { get; set; }
        public int question_id { get; set; }

        public virtual Question question { get; set; }
    }
}
