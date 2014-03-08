using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using XpandBlog.DTO.Security;

namespace XpandBlog.Web.Helpers.Security
{
    public static class UserExtentions
    {
        public static async Task<ClaimsIdentity> GenerateUserIdentityAsync(this User user, UserManager<User, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}