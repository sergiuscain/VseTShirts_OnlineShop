using VseTShirts.Models;

namespace VseTShirts.Areas.Admin.Models
{
    public class UserIndexViewModel
    {
        public List<UserViewModel> Users { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}
