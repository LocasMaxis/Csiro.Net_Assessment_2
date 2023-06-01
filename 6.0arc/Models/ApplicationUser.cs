using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Csiro.Models
{
    public class ApplicantionUser: IdentityUser
    {
   
        //public int ApplicantID { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string Email { get; set; }
        public int CourseID { get; set; } 
        public int UniID { get; set; } 
        public float Gpa { get; set; }
		public string? CoverLetter { get; set; }
		public string? Resume { get; set; }
	
    }
}
