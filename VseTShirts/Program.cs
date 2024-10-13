using VseTShirts.Models;
using VseTShirts.DB;
using Serilog;
using Microsoft.EntityFrameworkCore;

namespace VseTShirts
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connection = builder.Configuration.GetConnectionString("VseTShirts");
            object value = builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connection));

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient<ICartsStorage ,CartsDBStorage>();
            builder.Services.AddTransient<IProductsStorage ,ProductsDBStorage>();
            builder.Services.AddTransient<IFavoriteProductsStorage, FavoriteProductsDBStorage>();
            builder.Services.AddTransient<IOrdersStorage, OrdersDBStorage>();
            builder.Services.AddTransient<IComparedProductsStorage, ComparedProductsDBStorage>();
            builder.Services.AddSingleton<IAccountManager, AccountInMemoryManager>();
            builder.Services.AddSingleton<IRolesStorage, RolesInMemoryStorage>();
            builder.Host.UseSerilog((context, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .Enrich.WithProperty("ApplicationName", "Online Shop"));

            var app = builder.Build();

            app.UseSerilogRequestLogging();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "MyArea",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
//Views/Shared/Components/CalcCartCount/CalcCartCountViewComponent.cshtml