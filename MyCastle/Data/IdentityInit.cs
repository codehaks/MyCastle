using Microsoft.AspNetCore.Identity;
using MyCastle.Models;
using System;
using System.Threading.Tasks;

namespace Portal.Persistance
{
    public class IdentityInit
    {

        //public async Task SeedUsers(UserManager<ApplicationUser> userManager)
        //{
        //    var userName = "0913";

        //    if (userManager.FindByNameAsync(userName).Result == null)
        //    {
        //        var user = new ApplicationUser
        //        {
        //            UserName = userName,
        //            FirstName = "مدیر",
        //            LastName = "سایت",
        //            Email = userName
        //        };

        //        IdentityResult result = userManager.CreateAsync(user, "123@Pass").Result;

        //        if (result.Succeeded)
        //        {
        //            await userManager.AddToRoleAsync(user, "Admin");
        //        }
        //    }

        //}

        public async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Admin"
                };

                await roleManager.CreateAsync(role);
            }

           
            return;
        }
    }
}
