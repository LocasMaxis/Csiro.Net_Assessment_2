using Csiro.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCAssessment2.Models;

namespace _6._0arc.Models
{
    public class ApplicationCombined
    {
        public Applicants? a10 { get; set; }
        public Courses? c10 { get; set; }
        public Universities? u10 { get; set;}
    }
}
