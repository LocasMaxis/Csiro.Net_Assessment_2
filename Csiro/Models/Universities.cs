using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Csiro.Models
{
    public class Universities
    {
        [Key]
        [ForeignKey("Universites")]
        public int UniID { get; set; }

		public int UniRank { get; set; }

		public string UniName { get; set; }

    }
}
