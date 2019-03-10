using MyCastle.Models;
using System.Security.Claims;

namespace MyApp.Common
{
    public static class UserExtentions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public static UserProfile GetProfile(this ClaimsPrincipal user)
        {
            var profile = new UserProfile
            {
                FirstName = user.FindFirst("FirstName") != null ? user.FindFirst("FirstName").Value : string.Empty,
                LastName = user.FindFirst("LastName") != null ? user.FindFirst("LastName").Value : string.Empty,
               
            };

            return profile;
        }
    }

}
