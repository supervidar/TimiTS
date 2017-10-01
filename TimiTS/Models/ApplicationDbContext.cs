using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//The database context class is the bridge between the application and the EF Core and provides access to the
//application’s data using model objects.
//The DbContext base class provides access to the Entity Framework Core’s underlying functionality, and
//the Project property will provide access to the Project objects in the database.

namespace TimiTS.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Contractor> Contractors { get; set; }

        public DbSet<WorkParticipation> WorkParticipations { get; set; }
        public DbSet<WorkType> WorkTypes { get; set; }
        public DbSet<WorkCategory> WorkCategories { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<FeedbackCategory> FeedbackCategories { get; set; }


    }
}
