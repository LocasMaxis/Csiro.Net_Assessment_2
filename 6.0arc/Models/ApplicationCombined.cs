using Csiro.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Csiro.Models
{
    public class ApplicationCombined
    {
        public Applicants? a10 { get; set; }
        public Courses? c10 { get; set; }
        public Universities? u10 { get; set;}
    }
}
