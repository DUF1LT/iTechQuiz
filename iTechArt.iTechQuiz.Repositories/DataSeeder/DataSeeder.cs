using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iTechArt.iTechQuiz.Domain.Models;
using iTechArt.iTechQuiz.Repositories.Constants;
using iTechArt.iTechQuiz.Repositories.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.iTechQuiz.Repositories.DataSeeder
{
    public static class DataSeeder
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager, iTechQuizContext context)
        {
            string adminEmail = "admin@itechart.com";
            string adminPassword = "Adm!n2021";

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
                User admin = new User
                {
                    Email = adminEmail, 
                    UserName = Roles.Admin, 
                    RegisteredAt = DateTime.Now
                };

                IdentityResult result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, Roles.Admin);
                }
            }

            if (await userManager.Users.IgnoreQueryFilters().FirstOrDefaultAsync(u => u.Id == default) is null)
            {
                User anonymous = new User
                {
                    Id = default,
                    Email = "anonymous@itechart.com", 
                    UserName = "anonymous", 
                    IsSystemUser = true, 
                    RegisteredAt = DateTime.Now
                };

                await userManager.CreateAsync(anonymous);
            }

            if (!await context.QuestionTypes.AnyAsync())
            {
                var types = new List<QuestionType>
                {
                    new() { Name = "Single answer" },
                    new() { Name = "Multiple answer" },
                    new() { Name = "Text" },
                    new() { Name = "File" },
                    new() { Name = "Rating" },
                    new() { Name = "Scale" }
                };

                await context.AddRangeAsync(types);
                await context.SaveChangesAsync();
            }
        }
    }
}
