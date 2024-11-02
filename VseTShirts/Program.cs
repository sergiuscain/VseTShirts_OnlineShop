using VseTShirts.Models;
using VseTShirts.DB;
using Serilog;
using Microsoft.EntityFrameworkCore;
using VseTShirts.DB.Models;
using Microsoft.AspNetCore.Identity;
using VseTShirts.Helpers;

namespace VseTShirts
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            string connection = builder.Configuration.GetConnectionString("DefaultConnection");
            object value = builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer (connection));
            builder.Services.AddDbContext<IdentityContext>(options => options.UseSqlServer(connection));
            builder.Services.AddIdentity<User, IdentityRole>()
                           .AddEntityFrameworkStores<IdentityContext>();
            builder.Services.ConfigureApplicationCookie(options =>           //настраиваем куки
            {
                options.ExpireTimeSpan = TimeSpan.FromHours(12);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.Cookie = new CookieBuilder
                {
                    IsEssential = true
                };
            });
            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient<ICollectionsStorage, CollectionsDBStorage>();
            builder.Services.AddTransient<ICartsStorage ,CartsDBStorage>();
            builder.Services.AddTransient<IProductsStorage ,ProductsDBStorage>();
            builder.Services.AddTransient<IFavoriteProductsStorage, FavoriteProductsDBStorage>();
            builder.Services.AddTransient<IOrdersStorage, OrdersDBStorage>();
            builder.Services.AddTransient<ImageProvider>();
            builder.Services.AddTransient<IComparedProductsStorage, ComparedProductsDBStorage>();
            builder.Host.UseSerilog((context, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .Enrich.WithProperty("ApplicationName", "Online Shop"));


            var app = builder.Build();

            using (var serviceScope = app.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var userManager = services.GetRequiredService<UserManager<User>>();
                var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                IdentityInitializer.Initialize(userManager, rolesManager);
            }

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

            app.UseAuthentication();
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