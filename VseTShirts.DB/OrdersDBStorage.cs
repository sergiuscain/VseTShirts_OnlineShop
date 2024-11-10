using Microsoft.EntityFrameworkCore;
using VseTShirts.DB.Models;

namespace VseTShirts.DB
{
    public class OrdersDBStorage : IOrdersStorage
    {
        private readonly DatabaseContext _dbContext;
        public OrdersDBStorage(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync( Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
        }
        public async Task RemoveAsync( Order order )
        {
            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<Order>> GetAllAsync() => await _dbContext
            .Orders
            .Include(c => c.Positions)
            .ThenInclude(p => p.Product)
            .ToListAsync();

        public async Task<Order> GetByIdAsync(Guid id)
        {
            var order = await _dbContext.Orders
                .Include(c => c.Positions)
                .ThenInclude(p=>p.Product)
                .ThenInclude(o => o.Images)
                .FirstOrDefaultAsync(o => o.Id == id);
            return order;
        }

        public async Task UpdateStatusAsync(Guid id, OrderStatus status)
        {
            (await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id)).Status = status;;

            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveByIdAsync(Guid id)
        {
            var removedOrder = await _dbContext.Orders
                .Include(o => o.Positions)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.Images)
                .FirstOrDefaultAsync(o => o.Id == id);
            _dbContext.Orders.Remove(removedOrder);
            await _dbContext.SaveChangesAsync();
        }
    }
}
 