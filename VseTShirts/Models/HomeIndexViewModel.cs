using VseTShirts.Helpers;

namespace VseTShirts.Models
{
    public class HomeIndexViewModel
    {
        public List<ProductViewModel> Products { get; set; }
        public FiltersViewModel Filters { get; set; }
        public List<CollectionViewModel> CollectionsList { get; set; }
        public List<string> Colors { get; set; }
        public List<string> Sizes { get; set; }
        public List<string> Gender { get; set; }
        public List<string> Categories { get; set; }
        public bool IsActiveFilters { get; set; }
        public HomeIndexViewModel()
        {
            Colors = Helpers.Data.Color;
            Sizes = Helpers.Data.Size;
            Gender = Helpers.Data.Gender;
            Categories = Helpers.Data.Categories;
        }
    }
}
