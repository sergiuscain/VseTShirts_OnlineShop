using VseTShirts.DB.Models;

namespace VseTShirts.DB
{
    public interface ICollectionsStorage
    {
        void Add(Collection collection);
        void Clear();
        void Delete(string id);
        void Edit(string name, string newName, string description);
        List<Collection> GetAll();
    }
}