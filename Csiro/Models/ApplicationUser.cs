using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Csiro.Models
{
    public class ApplicationUser : IdentityUser
    {

        //public int ApplicantID { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }

        /*public float Gpa { get; set; }
        public string? CoverLetter { get; set; }
        public string? Resume { get; set; }


        public int CourseID { get; set; }
        [ForeignKey("CourseID")]
        public Courses Courses { get; set; }

        public int UniID { get; set; }
        [ForeignKey("UniID")]
        public Universities Universities { get; set; }*/
    }
}
