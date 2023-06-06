using System.ComponentModel.DataAnnotations;

namespace Csiro.Models
{
    public class Courses
    {
        [Key]
        public int CourseID { get; set; }

        public string CourseName { get; set; }


    }
}
