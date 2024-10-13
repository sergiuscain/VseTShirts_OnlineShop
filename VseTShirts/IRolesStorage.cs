using VseTShirts.Areas.Admin.Models;

namespace VseTShirts.Models
{
    public interface IRolesStorage
    {
        List<Role> GetAll();
        Role GetByName(string name);
        void Add(Role role);
        void RemoveAll();
        void Remove(Role role);
    }
}