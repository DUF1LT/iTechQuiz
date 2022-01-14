using System;
using System.Security.Claims;

namespace iTechArt.Common.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal principal)
        {
            if (principal is null)
            {
                throw new NullReferenceException(nameof(principal));
            }

            return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
