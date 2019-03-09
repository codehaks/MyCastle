using System.Security.Claims;

namespace MyApp.Common
{
    public static class UserExtentions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        //public static UserProfileModel GetProfile(this ClaimsPrincipal user)
        //{
        //    var profile = new UserProfileModel
        //    {
        //        FirstName = user.FindFirst("FirstName") != null ? user.FindFirst("FirstName").Value : string.Empty,
        //        LastName = user.FindFirst("LastName") != null ? user.FindFirst("LastName").Value : string.Empty,
        //        NationalCode = user.FindFirst("NationalCode") != null ? user.FindFirst("NationalCode").Value : string.Empty
        //    };

        //    return profile;
        //}
    }

}
