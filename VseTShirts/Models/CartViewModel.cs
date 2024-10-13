using VseTShirts.DB.Models;

namespace VseTShirts.Models
{
    public class CartViewModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<CartPositionViewModel> Positions { get; set; }
        public int productsCountInCart
        {
            get
            {
                return Positions.Sum(x => x.Quantity);
            }
        }
        public decimal price
        {
            get
            {
                return Positions.Sum(x => x.Price);
            }
        }
    }
}
