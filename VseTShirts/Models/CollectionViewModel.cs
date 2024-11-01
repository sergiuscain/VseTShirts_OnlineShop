namespace VseTShirts.Models
{
    public class CollectionViewModel
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
