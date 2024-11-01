namespace VseTShirts.Models
{
    public class HomeIndexViewModel
    {
        public List<ProductViewModel> Products { get; set; }
        public FiltersViewModel Filters { get; set; }
        public List<CollectionViewModel> CollectionsList { get; set; }
    }
}
