

namespace VseTShirts.DB.Models
{
    public class Product
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; } 
        public int Quantity { get; set; } 
        public string Sex { get; set; } 
        public string Category { get; set; } 
        public string Color { get; set; }
        public string Size { get; set; }
        public List<CartPosition> CartPositions { get; set; }
        public List<Image> Images { get; set; }
    }
}