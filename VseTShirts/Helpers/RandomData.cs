using System.Drawing;
using VseTShirts.DB.Models;

namespace VseTShirts.Helpers
{
    public static class RandomData
    {
        private static Random random = new Random();
        private static List<string> _sex = new List<string>
        {
            "Female",
            "Male"
        };
        private static List<string> _randomProductSize = new List<string>
        {
            "S",
            "M",
            "L",
            "XL",
            "XXL"
        };
        private static List<string> _randomImages = new List<string>
        {
            "1.jpg",
            "2.jpg",
            "3.jpg",
            "4.jpg",
            "5.jpg",
            "6.jpg",
            "7.jpg"
        };
        private static List<string> _randomProductDescription = new List<string>
        {
            "A stylish, comfortable, and breathable ",
            "A well-fitted, stylish, and comfortable ",
            "A sleek, stylish, and breathable ",
            "A well-fitted, stylish, and comfortable ",
            "A stylish, comfortable, and breathable ",
            "A sleek, stylish, and breathable ",
            "A well-fitted, stylish, and comfortable ",
        };
        private static List<string> _randomProductName = new List<string>
        {
            "Black T-shirt",
            "White T-shirt",
            "Blue T-shirt",
            "Green T-shirt",
            "Red T-shirt",
            "Black Jeans",
            "White Jeans",
            "Blue Jeans",
            "Green Jeans",
            "Red Jeans",
            "Black Hat",
            "White Hat",
            "Blue Hat",
            "Green Hat",
            "Red Hat",
            "Black Scarf",
            "White Scarf",
            "Blue Scarf",
            "Green Scarf",
            "Red Scarf",
            "Black Jacket",
            "White Jacket",
            "Blue Jacket",
            "Green Jacket",
            "Red Jacket",
            "Black Sweater",
            "White Sweater",
            "Blue Sweater",
            "Green Sweater",
            "Red Sweater",
            "Black Coat",
            "White Coat",
            "Blue Coat",
            "Green Coat",
            "Red Coat",
            "Black Socks",
            "White Socks",
            "Blue Socks",
            "Green Socks",
            "Red Socks",
            "Black Bag",
            "White Bag",
            "Blue Bag",
            "Green Bag",
            "Red Bag",
        };
        private static List<string> collections = new List<string>
        {
            "Аниме",
            "Древние боги",
            "LoL",
            "DevilMayCry",
            "GodOfWar"

        };

        public static List<string> RandomProductName { get => _randomProductName; set => _randomProductName = value; }
        public static List<string> RandomImages { get => _randomImages; set => _randomImages = value; }

        public static string GetName()
        {
            return _randomProductName[random.Next(0, _randomProductName.Count)];
        }
        public static string GetDescription()
        {
            return _randomProductDescription[random.Next(0, _randomProductDescription.Count)];
        }
        public static string GetSize() => _randomProductSize[random.Next(0, _randomProductSize.Count)];
        public static decimal GetPrice() => random.Next(900, 19990);
        public static int GetQuantity() => random.Next(1, 1000);
        public static string GetSex() => _sex[random.Next(0, _sex.Count)];
        public static List<string> GetProductImagePath(string sex ,string category) => new List<string> { $"/Images/RandomProduct/{sex}/{category}/" +_randomImages[random.Next(0, _randomImages.Count)]};

        public static string GetCollection()
        {
            return collections[random.Next(0, collections.Count)];
        }
    }
}