using VseTShirts.DB.Models;

namespace VseTShirts.DB
{
    public interface IOrdersStorage
    {
        void Add( Order order);
        void Remove( Order order );
        List<Order> GetAll();
        Order GetById(Guid id);
        void UpdateStatus(Guid id, OrderStatus status);
        void RemoveById(Guid id);
    }
}