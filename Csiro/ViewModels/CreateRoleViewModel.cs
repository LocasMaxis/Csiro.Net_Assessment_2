using System.ComponentModel.DataAnnotations;

namespace Csiro.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
