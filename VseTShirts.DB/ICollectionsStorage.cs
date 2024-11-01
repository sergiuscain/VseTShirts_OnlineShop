using VseTShirts.DB.Models;

namespace VseTShirts.DB
{
    public interface ICollectionsStorage
    {
        void Add(Collection collection);
        void Clear();
        void Delete(int id);
        void Edit(int id, Collection newCollection);
        List<Collection> GetAll();
    }
}