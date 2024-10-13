using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VseTShirts.DB.Models;

namespace VseTShirts.DB
{
    public static class Mapping
    {
        public static Product ToProduct(ComparedProduct compared)
        {
            Product product = new Product
            {
                Id = compared.product.Id,
                Name = compared.product.Name,
                Description = compared.product.Description,
                Price = compared.product.Price,
                CartPositions = compared.product.CartPositions,
                ImagePath = compared.product.ImagePath,
                Category = compared.product.Category,
                Color = compared.product.Color,
                Size = compared.product.Size,
                Quantity = compared.product.Quantity,
                Sex = compared.product.Sex,
            };
            return product;
        }

        public static List<Product> ToProducts(List<ComparedProduct> comparedProducts)
        {
            var products = new List<Product>();
            foreach (var compared in comparedProducts)
            {
                products.Add(ToProduct(compared));
            }
            return products;
        }

    }
}
