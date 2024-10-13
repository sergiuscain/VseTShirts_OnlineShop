using VseTShirts.Areas.Admin.Models;

namespace VseTShirts.Models
{
    public class RolesInMemoryStorage : IRolesStorage
    {
        private readonly List<Role> roles = new List<Role>();

        public void Add(Role role) => roles.Add(role);

        public List<Role> GetAll() => roles;

        public Role GetByName(string name) => roles.FirstOrDefault(r => r.Name == name);

        public void Remove(Role role) => roles.Remove(role);

        public void RemoveAll() => roles.Clear();
    }
}