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
        public string? State { get; set; }

        public string? Address { get; set; }

        public string? Postcode { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }


    }
}
