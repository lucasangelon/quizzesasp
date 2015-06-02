using MySql.Data.Entity;
using Quizzes7.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzes7.DAL
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class QuizzesContext : DbContext
    {
        public QuizzesContext() : base("QuizzesContext")
        {

        }

        public DbSet<User> user { get; set; }
        public DbSet<Role> role { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
