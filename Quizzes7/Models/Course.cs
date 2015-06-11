﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzes7.Models
{
    public class Course
    {
        public int id { get; set; }
        public string name { get; set; }

        public virtual ICollection<Cluster> clusters { get; set; }
    }
}
