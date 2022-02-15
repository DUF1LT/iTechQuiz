using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace iTechArt.Common.Extensions
{
    public static class SignInManagerExtensions
    {
        public static async Task<SignInResult> PasswordEmailSignInAsync<TUser>(this SignInManager<TUser> signInManager,
            string email,
            string password,
            bool isPersistent,
            bool shouldLockout) where TUser : IdentityUser<Guid>
        {
            var user = await signInManager.UserManager.FindByEmailAsync(email);
            if (user != null)
            {
                return await signInManager.PasswordSignInAsync(user.UserName, password, isPersistent, shouldLockout);
            }

            return SignInResult.Failed;
        }
    }
}
