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


        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.Migrate();
        }
    }
}
