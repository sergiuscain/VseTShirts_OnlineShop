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

        public void Add( Order order)
        {
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
        }
        public void Remove( Order order )
        {
            _dbContext.Orders.Remove(order);
            _dbContext.SaveChanges();
        }
        public List<Order> GetAll() => _dbContext
            .Orders
            .Include(c => c.Positions)
            .ThenInclude(p => p.Product)
            .ToList();

        public Order GetById(Guid id)
        {
            var order = _dbContext.Orders
                .Include(c => c.Positions)
                .ThenInclude(p=>p.Product)
                .ThenInclude(o => o.Images)
                .FirstOrDefault(o => o.Id == id);
            return order;
        }

        public void UpdateStatus(Guid id, OrderStatus status)
        {
            _dbContext.Orders.FirstOrDefault(o=> o.Id == id).Status = status;
            _dbContext.SaveChanges();
        }

        public void RemoveById(Guid id)
        {
            var removedOrder = _dbContext.Orders
                .Include(o => o.Positions)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.Images)
                .FirstOrDefault(o => o.Id == id);
            _dbContext.Orders.Remove(removedOrder);
            _dbContext.SaveChanges();
        }
    }
}
 