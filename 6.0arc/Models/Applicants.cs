using System.ComponentModel.DataAnnotations;

namespace Csiro.Models
{
    public class Applicants
    {
        [Key]
        public int applicantID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public int courseID { get; set; }
        public int uniID { get; set; }
        public float gpa { get; set; }
        public string resume { get; set; }
        public int userID { get; set; }
    }
}
