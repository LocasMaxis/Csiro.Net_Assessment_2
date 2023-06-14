using Microsoft.AspNetCore.Mvc.Rendering;

namespace Csiro.ViewModels
{
    public class ManageRole
    {
        public string userID { get; set; }
        public string roleID { get; set; }

        public List<SelectListItem> userList = new List<SelectListItem>();
        public List<SelectListItem> roleList = new List<SelectListItem>();


    }
}
