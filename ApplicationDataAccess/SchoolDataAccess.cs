using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationModel;

namespace ApplicationDataAccess
{
    public class SchoolDataAccess : DbContext
    {
        public SchoolDataAccess() : base("SchoolDbContext")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<SchoolDataAccess>(null);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Admission> Admissions { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Standard> Standards { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }
    }
}
