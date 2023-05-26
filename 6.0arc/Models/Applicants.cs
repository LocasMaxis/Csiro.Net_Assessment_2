using System.ComponentModel.DataAnnotations;

namespace Csiro.Models
{
    public class Applicants
    {
        [Key]
        public int applicantID { get; set; } //pk
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public int courseID { get; set; } //fk
        public int uniID { get; set; } //fk
        public float gpa { get; set; }
		public string? coverLetter { get; set; }
		public string? resume { get; set; }
		public int UserId { get; set; } // fk - from AspNetUsers
    }
}
