namespace VseTShirts.Models
{
    public class FiltersViewModel
    {
        public decimal StartPrice { get; set; }
        public decimal EndPrice { get; set; }
        public decimal MinQuantity { get; set; }
        public decimal MaxQuantity { get; set; }
        public string Sex { get; set; }
        public string Category { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string SortBy { get; set; }
    }
}
