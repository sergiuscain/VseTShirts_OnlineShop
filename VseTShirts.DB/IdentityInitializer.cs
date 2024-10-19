using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VseTShirts.DB.Models;

namespace VseTShirts.DB
{
    public class IdentityInitializer
    {
        public static void Initialize(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminEmail = "admin@admin.com";
            var adminPassword = "_Admon001@1";
            if (roleManager.FindByNameAsync(Constants.AdminRoleName).Result == null)
            {
                roleManager.CreateAsync(new IdentityRole(Constants.AdminRoleName)).Wait();
            }
            if (roleManager.FindByNameAsync(Constants.UserRoleName).Result == null)
            {
                roleManager.CreateAsync(new IdentityRole(Constants.UserRoleName)).Wait();
            }
            if (userManager.FindByNameAsync(adminEmail).Result == null)
            {
                var user = new User { UserName = adminEmail, Email = adminEmail };
                var result = userManager.CreateAsync(user, adminPassword).Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Constants.AdminRoleName).Wait();
                }
            }
        }
    }
}
