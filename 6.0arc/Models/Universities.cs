﻿using System.ComponentModel.DataAnnotations;

namespace MVCAssessment2.Models
{
    public class Universities
    {
        [Key]
        public int uniID { get; set; }

        public string uniName { get; set; }

    }
}
