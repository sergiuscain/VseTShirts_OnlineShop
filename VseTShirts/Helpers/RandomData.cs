using System.Drawing;
using VseTShirts.DB.Models;

namespace VseTShirts.Helpers
{
    public static class RandomData
    {
        private static Random random = new Random();
        private static List<string> _sex = new List<string>
        {
            "FEMALE",
            "MALE"
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
            "BLACK T-SHIRT",
            "WHITE T-SHIRT",
            "BLUE T-SHIRT",
            "GREEN T-SHIRT",
            "RED T-SHIRT",
            "BLACK JEANS",
            "WHITE JEANS",
            "BLUE JEANS",
            "GREEN JEANS",
            "RED JEANS",
            "BLACK HAT",
            "WHITE HAT",
            "BLUE HAT",
            "GREEN HAT",
            "RED HAT",
            "BLACK SCARF",
            "WHITE SCARF",
            "BLUE SCARF",
            "GREEN SCARF",
            "RED SCARF",
            "BLACK JACKET",
            "WHITE JACKET",
            "BLUE JACKET",
            "GREEN JACKET",
            "RED JACKET",
            "BLACK SWEATER",
            "WHITE SWEATER",
            "BLUE SWEATER",
            "GREEN SWEATER",
            "RED SWEATER",
            "BLACK COAT",
            "WHITE COAT",
            "BLUE COAT",
            "GREEN COAT",
            "RED COAT",
            "BLACK SOCKS",
            "WHITE SOCKS",
            "BLUE SOCKS",
            "GREEN SOCKS",
            "RED SOCKS",
            "BLACK BAG",
            "WHITE BAG",
            "BLUE BAG",
            "GREEN BAG",
            "RED BAG",
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
    }
}