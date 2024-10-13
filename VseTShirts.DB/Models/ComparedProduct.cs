
namespace VseTShirts.DB.Models
{
    public class ComparedProduct
    {
        public string UserId { get; set; }
        public Product product  { get; set; }
        public Guid Id { get; set; } 
    }
}
