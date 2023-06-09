﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Csiro.Models
{
    public class Applicants
    {
        [Key]
        [Display(Name = "Application ID")]
        public int applicantID { get; set; }

        [Display(Name = "First Name")]
        public string firstName { get; set; }
        [Display(Name = "Last Name")]
        public string lastName { get; set; }
        [Display(Name = "Email")]
        public string email { get; set; }
        public int courseID { get; set; }
        [ForeignKey("courseID")]
        public virtual Courses Courses { get; set; }
        public int uniID { get; set; }
        [ForeignKey("uniID")]
        public virtual Universities Universities { get; set; }
        [Display(Name = "GPA Score")]
        public float gpa { get; set; }
        [Display(Name = "Cover Letter")]
        public string? coverLetter { get; set; }
        [Display(Name = "Resume")]
        public string resume { get; set; }
        public string userId { get; set; }
        [ForeignKey("userId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public bool sent { get; set; }
        public bool deleted { get; set; }

 



        public Applicants()
        {
            sent = false;
            deleted = false;
        }

    }
}
