using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VseTShirts.DB.Models
{
    public class FiltersModel
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
