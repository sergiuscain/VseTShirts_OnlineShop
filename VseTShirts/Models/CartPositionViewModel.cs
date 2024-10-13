namespace VseTShirts.Models
{
    public class CartPositionViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ProductViewModel? Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price
        {
            get
            {
                return Product.Price* Quantity;
            }
        }

    }
}
