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
        public DbSet<FAQ> faq { get; set; }
        public DbSet<Course> course { get; set; }
        public DbSet<Cluster> cluster { get; set; }
        public DbSet<Unit> unit { get; set; }
        public DbSet<Quiz> quiz { get; set; }
        public DbSet<Language> language { get; set; }
        public DbSet<Quizzes7.Models.Type> type { get; set; }
        public DbSet<Specific> specific { get; set; }
        public DbSet<Question> question { get; set; }
        public DbSet<QuestionExtra> question_extra { get; set; }
        public DbSet<UserQuizAnswer> user_quiz_answer { get; set; }
        public DbSet<UserQuiz> user_quiz { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Course>()
                .HasMany(c => c.clusters).WithMany(c => c.courses)
                .Map(t => t.MapLeftKey("course_id")
                    .MapRightKey("cluster_id")
                    .ToTable("course_cluster"));

            modelBuilder.Entity<Cluster>()
                .HasMany(c => c.units).WithMany(c => c.clusters)
                .Map(t => t.MapLeftKey("cluster_id")
                    .MapRightKey("unit_id")
                    .ToTable("cluster_unit"));

            modelBuilder.Entity<Unit>()
                .HasMany(u => u.users).WithMany(u => u.units)
                .Map(t => t.MapLeftKey("unit_id")
                    .MapRightKey("user_id")
                    .ToTable("user_unit"));
        }
    }
}
