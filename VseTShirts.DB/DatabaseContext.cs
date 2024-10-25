using Microsoft.EntityFrameworkCore;
using VseTShirts.DB.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VseTShirts.DB
{
    public class DatabaseContext : DbContext
    {
        //Доступ к таблицам:
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartPosition> CartPositions { get; set; }
        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }
        public DbSet<ComparedProduct> ComparedProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Image> Images { get; set; }


        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>()
                .HasOne(p => p.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            var Product1Id = Guid.Parse("92bced76-82ba-4f44-af74-70eb7b31a6f9");
            var Product2Id = Guid.Parse("ba7aec10-45d0-49ad-8ee6-ddbe95371796");
            var image1 = new Image
            {
                Id = Guid.Parse("c96dc613-1372-4746-87d7-47fed78a990b"),
                URL = "/Images/Products/Image1.jpg",
                ProductId = Product1Id
            };
            var image2 = new Image
            {
                Id = Guid.Parse("68bfe1d6-a659-4407-aa2a-d38b10af42b1"),
                URL = "/Images/Products/Image2.jpg",
                ProductId = Product2Id
            };
            modelBuilder.Entity<Image>().HasData(image1, image2);
            modelBuilder.Entity<Product>().HasData(new List<Product>()
            {
                new Product
                {
                    Id = Product1Id,
                    Name = "Product 1",
                    Description = "Description 1",
                    Price = 100,
                    Quantity = 10,
                    Sex = "Male",
                    Category = "Category 1",
                    Color = "Red",
                    Size = "S",
                },
                new Product
{
                    Id = Product2Id,
                    Name = "Product 1",
                    Description = "Description 1",
                    Price = 100,
                    Quantity = 10,
                    Sex = "Male",
                    Category = "Category 1",
                    Color = "Red",
                    Size = "S",
                }
            }); 
        } 
    }
}