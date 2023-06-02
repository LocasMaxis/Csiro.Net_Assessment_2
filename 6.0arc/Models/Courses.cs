using System.ComponentModel.DataAnnotations;

namespace Csiro.Models
{
    public class Courses
    {
        [Key]
        public int courseID { get; set; }

        public string courseName { get; set; }
    }
}
