using VseTShirts.Areas.Admin.Models;

namespace VseTShirts.Models
{
    public class RolesInMemoryStorage : IRolesStorage
    {
        private readonly List<RoleViewModel> roles = new List<RoleViewModel>();

        public void Add(RoleViewModel role) => roles.Add(role);

        public List<RoleViewModel> GetAll() => roles;

        public RoleViewModel GetByName(string name) => roles.FirstOrDefault(r => r.Name == name);

        public void Remove(RoleViewModel role) => roles.Remove(role);

        public void RemoveAll() => roles.Clear();
    }
}
