using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iTechArt.iTechQuiz.Domain.Models;
using iTechArt.iTechQuiz.Repositories.Constants;
using iTechArt.iTechQuiz.Repositories.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ObjectPool;

namespace iTechArt.iTechQuiz.Repositories
{
    public static class DataSeeder
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<Role> roleManager, iTechQuizContext context)
        {
            string adminEmail = "admin@itechart.com";
            string adminPassword = "Adm!n2021";

            if (await roleManager.FindByNameAsync(Roles.Admin) is null)
            {
                await roleManager.CreateAsync(new Role(Roles.Admin));
            }

            if (await roleManager.FindByNameAsync(Roles.User) is null)
            {
                await roleManager.CreateAsync(new Role(Roles.User));
            }

            if (await userManager.FindByNameAsync("admin") is null)
            {
                User admin = new User
                {
                    Id = Guid.NewGuid(),
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

            if (await userManager.Users.IgnoreQueryFilters().FirstOrDefaultAsync(p => p.Id == default) is null)
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
                var types = new List<QuestionTypeLookup>
                {
                    new() {Id = QuestionType.SingleAnswer, Name = "Single answer" },
                    new() {Id = QuestionType.MultipleAnswer, Name = "Multiple answer" },
                    new() {Id = QuestionType.TextAnswer, Name = "Text answer" },
                    new() {Id = QuestionType.File, Name = "File" },
                    new() {Id = QuestionType.Rating, Name = "Rating" },
                    new() {Id = QuestionType.Scale, Name = "Scale" }
                };

                await context.AddRangeAsync(types);
                await context.SaveChangesAsync();
            }
        }
    }
}
