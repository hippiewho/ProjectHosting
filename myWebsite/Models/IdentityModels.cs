using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;


namespace myWebsite.Models
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            return await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        }

        public static explicit operator ApplicationUser(UserManager<ApplicationUser, string> v)
        {
            throw new NotImplementedException();
        }
    }

    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext() : base("LoginContext",throwIfV1Schema: false) { }

        public static ApplicationDBContext Create()
        {
            return new ApplicationDBContext();
        }
    }

}