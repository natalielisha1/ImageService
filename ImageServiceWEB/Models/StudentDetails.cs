/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex3
 */
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace ImageServiceWEB.Models
{
    /// <summary>
    /// Class StudentDetails.
    /// </summary>
    public class StudentDetails
    {
        //Key
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }
        //Real ID
        /// <summary>
        /// Gets or sets the isr identifier.
        /// </summary>
        /// <value>The isr identifier.</value>
        public int IsrID { get; set; }
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }
    }

    /// <summary>
    /// Class StudentDetailsDbContext.
    /// </summary>
    public class StudentDetailsDbContext : DbContext
    {
        /// <summary>
        /// Gets or sets the students.
        /// </summary>
        /// <value>The students.</value>
        public DbSet<StudentDetails> Students { get; set; }

        /// <summary>
        /// Initializes a new instance of the StudentDetailsDbContext class.
        /// </summary>
        public StudentDetailsDbContext(): base("Students")
        {
            this.Database.CommandTimeout = 60;
        }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.</remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<StudentDetailsDbContext>(new CreateDatabaseIfNotExists<StudentDetailsDbContext>());
            base.OnModelCreating(modelBuilder);
        }
    }
}