using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Csiro.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "First Name:")]
        [Required(ErrorMessage = "First Name is required")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name:")]
        [Required(ErrorMessage = "Last Name is required")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Display(Name = "GPA Score:")]
        [Required(ErrorMessage = "GPA is required")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "GPA must be a number with up to 2 decimal places")]
        [Range(3.0, (double)decimal.MaxValue, ErrorMessage = "GPA must be greater than 3 to register")]
        [DataType(DataType.Text)]
        public decimal Gpa { get; set; }

        [Display(Name = "Email:")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Password:")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password:")]
        [Required(ErrorMessage = "Re-enter password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirm password do not match")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Cover Letter (optional):")]
        [DataType(DataType.MultilineText)]
        public string CoverLetter { get; set; }

        [Display(Name = "Resume (optional):")]
        [DataType(DataType.MultilineText)]
        public string Resume { get; set; }

        [Display(Name = "Course:")]
        public int CourseID { get; set; }
        [Required(ErrorMessage = "Course is required")]
        public List<SelectListItem> courseList { get; set; }

        [Required(ErrorMessage = "University selection required")]
        public int UniID { get; set; }
        public List<SelectListItem> uniList { get; set; }

        public RegisterViewModel()
        {
            courseList = new List<SelectListItem>();
            uniList = new List<SelectListItem>();
        }
    }
}