
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Csiro.Models
{
    public class ApplicantDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Applicants> applicants { get; set; }
		public DbSet<Courses> Courses { get; set; }
		public DbSet<Universities> Universities { get; set; }


        public ApplicantDbContext(DbContextOptions<ApplicantDbContext> options): base(options)
        {
            Database.EnsureCreated();
        } 
    }
}
