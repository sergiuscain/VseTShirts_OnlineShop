using System.Drawing;
using VseTShirts.DB.Models;

namespace VseTShirts.Helpers
{
    public static class RandomData
    {
        private static Random random = new Random();
        private static List<string> _sex = new List<string>
        {
            "Male",
            "Female"
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
            "https://cdn1.ozone.ru/s3/multimedia-8/6105537860.jpg"
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
            "Black T-Shirt",
            "White T-Shirt",
            "Blue T-Shirt",
            "Green T-Shirt",
            "Red T-Shirt",
            "Black blouses",
            "White blouses",
            "Blue Shiblousesrt",
            "Green blouses",
            "Red blouses",
            "Black jeans",
            "White jeans",
            "Blue jeans",
            "Green jeans",
            "Red jeans",
            "Black shoes",
            "White shoes",
            "Blue shoes",
            "Green shoes",
            "Red shoes",
            "Black hat",
            "White hat",
            "Blue hat",
            "Green hat",
            "Red hat",
            "Black scarf",
            "White scarf",
            "Blue scarf",
            "Green scarf",
            "Red scarf",
            "Black jacket",
            "White jacket",
            "Blue jacket",
            "Green jacket",
            "Red jacket",
            "Black sweater",
            "White sweater",
            "Blue sweater",
            "Green sweater",
            "Red sweater",
            "Black coat",
            "White coat",
            "Blue coat",
            "Green coat",
            "Red coat",
            "Black socks",
            "White socks",
            "Blue socks",
            "Green socks",
            "Red socks",
            "Black shirt",
            "White shirt",
            "Blue shirt",
            "Green shirt",
            "Red shirt",
            "Black bag",
            "White bag",
            "Blue bag",
            "Green bag",
            "Red bag",
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
        public static List<string> GetProductImagePath() => new List<string> { _randomImages[random.Next(0, _randomImages.Count)] };
    }
}