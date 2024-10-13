
namespace VseTShirts.DB.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public OrderStatus Status { get; set; }

        public DateTime DateAndTime { get; set; }

        public List<CartPosition> Positions { get; set; }
    }
}
