using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTechArt.iTechQuiz.Repositories.Constants;
using Microsoft.AspNetCore.Identity;

namespace iTechArt.iTechQuiz.Repositories.DataSeeder
{
    public static class DataSeeder
    {
        public static async Task InitializeAsync(UserManager<IdentityUser<Guid>> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            string adminEmail = "admin@itechart.com";
            string password = "Adm!n2021";

            if (await roleManager.FindByNameAsync(Roles.Admin) is null)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(Roles.Admin));
            }
            if (await roleManager.FindByNameAsync(Roles.User) is null)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(Roles.User));
            }
            if (await userManager.FindByNameAsync("admin") is null)
            {
                IdentityUser<Guid> admin = new IdentityUser<Guid> { Email = adminEmail, UserName = Roles.Admin };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, Roles.Admin);
                }
            }
        }
    }
}
