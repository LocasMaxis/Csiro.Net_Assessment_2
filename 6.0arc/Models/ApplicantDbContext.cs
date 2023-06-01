
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Csiro.Models
{
    public class ApplicantDbContext : IdentityDbContext<ApplicantionUser>
    {
        public DbSet<Applicants> applicants { get; set; }
		public DbSet<Courses> courses { get; set; }
		public DbSet<Universities> universities { get; set; }


        public ApplicantDbContext(DbContextOptions<ApplicantDbContext> options): base(options)
        {
            Database.EnsureCreated();
        } 
    }
}
