using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Csiro.ViewModels
{
    public class EditProfileViewModel //To Edit User profile
    {
        [Required]
        [Display(Name = "First Name*:")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name*:")]
        public string LastName { get; set; }

        public string? State { get; set; }

        public string? Address { get; set; }

        public string? Postcode { get; set; }

        public string? PhoneNumber { get; set;}

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
    }
}