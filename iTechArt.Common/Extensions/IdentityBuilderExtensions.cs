using iTechArt.iTechQuiz.Foundation.Providers;
using Microsoft.AspNetCore.Identity;

namespace iTechArt.Common.Extensions
{
    public static class IdentityBuilderExtensions
    {
        public static IdentityBuilder AddPasswordResetTokenProvider(this IdentityBuilder builder)
        {
            var userType = builder.UserType;
            var provider = typeof(PasswordResetTokenProvider<>).MakeGenericType(userType);
            return builder.AddTokenProvider("PasswordReset", provider);
        }
    }
}
