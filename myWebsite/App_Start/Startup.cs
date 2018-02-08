using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Owin;

namespace myWebsite.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                LoginPath = new PathString("/Login/Login"),
                LogoutPath = new PathString("/Login/Logout"),
                CookieName = ".LoginCred",
                SlidingExpiration = true,
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                AuthenticationMode = AuthenticationMode.Active

            });

        }
    }
}