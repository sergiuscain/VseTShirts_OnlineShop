using VseTShirts.DB.Models;

namespace VseTShirts.DB
{
    public interface ICollectionsStorage
    {
        Task AddAsync(Collection collection);
        Task ClearAsync();
        Task DeleteAsync(string id);
        Task EditAsync(string name, string newName, string description);
        Task<List<Collection>> GetAllAsync();
    }
}