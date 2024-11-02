using VseTShirts.Models;

namespace VseTShirts.Areas.Admin.Models
{
    public class ProductsOfCollection
    {
        public string Name {  get; set; }
        public List<ProductViewModel> ProductsInCollection { get; set; }
        public List<ProductViewModel> ProductsNotInCollection { get; set; }
    }
}
