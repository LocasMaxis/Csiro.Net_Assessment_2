using System.ComponentModel.DataAnnotations;

namespace Csiro.Models
{
    public class Universities
    {
        [Key]
        public int UniID { get; set; }

		public int UniRank { get; set; }

		public string UniName { get; set; }

    }
}
