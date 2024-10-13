using VseTShirts.DB.Models;

namespace VseTShirts.DB
{
    public interface ICartsStorage
    {
        void Add(Guid productId, string userId);
        void RemoveAll(string userId);
        void Remove(Guid productId, string userId);
        Cart GetCartByUserId(string userId);
        void RemovePosition(Guid productId,string userId);
    }
}