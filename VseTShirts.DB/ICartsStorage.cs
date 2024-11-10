using VseTShirts.DB.Models;

namespace VseTShirts.DB
{
    public interface ICartsStorage
    {
        Task AddAsync(Guid productId, string userId);
        Task RemoveAllAsync(string userId);
        Task RemoveAsync(Guid productId, string userId);
        Task<Cart> GetCartByUserIdAsync(string userId);
        Task RemovePositionAsync(Guid productId,string userId);
    }
}