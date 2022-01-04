using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace iTechArt.Common.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<string> GetFirstUserRoleAsync<TUser>(this UserManager<TUser> userManager, TUser user) where TUser : class
        {
            var roles = await userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            if (role is null)
            {
                throw new NullReferenceException();
            }

            return role;
        }

        public static async Task<bool> UserIsAdminAsync<TUser>(this UserManager<TUser> userManager, TUser user, string admin) where TUser : class
        {
            var role = await userManager.GetFirstUserRoleAsync(user);

            if (role.Equals(admin))
            {
                return true;
            }

            return false;
        }
    }
}
