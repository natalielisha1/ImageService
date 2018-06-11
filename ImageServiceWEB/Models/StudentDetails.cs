using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace ImageServiceWEB.Models
{
    public class StudentDetails
    {
        //Key
        public int Id { get; set; }
        //Real ID
        public int IsrID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class StudentDetailsDbContext : DbContext
    {
        public DbSet<StudentDetails> Students { get; set; }

        public StudentDetailsDbContext(): base("Students")
        {
            this.Database.CommandTimeout = 60;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<StudentDetailsDbContext>(new CreateDatabaseIfNotExists<StudentDetailsDbContext>());
            base.OnModelCreating(modelBuilder);
        }
    }
}