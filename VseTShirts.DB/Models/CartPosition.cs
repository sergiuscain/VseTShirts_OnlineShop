namespace VseTShirts.DB.Models
{
    public class CartPosition
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
