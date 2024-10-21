using VseTShirts.Areas.Admin.Models;

namespace VseTShirts.Models
{
    public interface IRolesStorage
    {
        List<RoleViewModel> GetAll();
        RoleViewModel GetByName(string name);
        void Add(RoleViewModel role);
        void RemoveAll();
        void Remove(RoleViewModel role);
    }
}