
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Csiro.Models
{
    public class ApplicantDataContext : IdentityDbContext
    {
        public DbSet<Applicants> applicants { get; set; }
        public DbSet<Courses> courses { get; set; }
        public DbSet<Universities> universities { get; set; }

        public ApplicantDataContext(DbContextOptions<ApplicantDataContext> options): base(options)
        {
            Database.EnsureCreated();
        } 
    }
}
