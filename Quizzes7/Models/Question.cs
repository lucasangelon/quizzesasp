using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzes7.Models
{
    public class Question
    {
        public int id { get; set; }
        public string content { get; set; }
        public int quiz_id { get; set; }
        public int type_id { get; set; }
        public int language_id { get; set; }
        public int specific_id { get; set; }
        public string correct_answer { get; set; }

        public virtual Quiz quiz { get; set; }
        public virtual User user { get; set; }
        public virtual Quizzes7.Models.Type type { get; set; }
        public virtual Language language { get; set; }
        public virtual Specific specific { get; set; }

        public ICollection<QuestionExtra> question_extras { get; set; }
    }
}
