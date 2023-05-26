using System.ComponentModel.DataAnnotations;

namespace Csiro.Models
{
    public class Universities
    {
        [Key]
        public int uniID { get; set; }

		public int uniRank { get; set; }

		public string uniName { get; set; }

    }
}
