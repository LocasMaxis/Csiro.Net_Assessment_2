using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Csiro.ViewModels
{
    public class ApplicantsViewModel //To submit Applications 
    {
        [Display(Name = "Applicant ID:")]
        public int ApplicantID { get; set; }

        [Display(Name = "First Name:")]
        [Required(ErrorMessage = "First Name is required")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name:")]
        [Required(ErrorMessage = "Last Name is required")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Display(Name = "Email:")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "GPA Score:")]
        [Required(ErrorMessage = "GPA is required")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "GPA must be a number with up to 2 decimal places")]
        [Range(3.0, (double)decimal.MaxValue, ErrorMessage = "GPA must be greater than 3 to register")]
        [DataType(DataType.Text)]
        public float Gpa { get; set; }

        [Display(Name = "Cover Letter (optional):")]
        public string? CoverLetter { get; set; }

        [Display(Name = "Resume (optional):")]
        public string? Resume { get; set; }

        [Display(Name = "Course:")]
        [Required(ErrorMessage = "Course is required")]
        public int CourseID { get; set; }

        public List<SelectListItem> courseList { get; set; }

        [Required(ErrorMessage = "University selection required")]
        public int UniID { get; set; }

        public List<SelectListItem> uniList { get; set; }

        public ApplicantsViewModel()
        {
            courseList = new List<SelectListItem>();
            uniList = new List<SelectListItem>();
        }
    }
}
