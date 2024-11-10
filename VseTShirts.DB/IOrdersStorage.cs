using VseTShirts.DB.Models;

namespace VseTShirts.DB
{
    public interface IOrdersStorage
    {
        Task AddAsync( Order order);
        Task RemoveAsync( Order order );
        Task<List<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(Guid id);
        Task UpdateStatusAsync(Guid id, OrderStatus status);
        Task RemoveByIdAsync(Guid id);
    }
}