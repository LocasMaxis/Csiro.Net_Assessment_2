using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Csiro.Models
{
    public class Courses
    {
        [Key]
        [ForeignKey("Courses")]
        public int CourseID { get; set; }

        public string CourseName { get; set; }


    }
}
