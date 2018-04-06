using myWebsite.App_Start;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace myWebsite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer<myWebsite.Models.LoginContext>(null);
            System.Web.Helpers.AntiForgeryConfig.UniqueClaimTypeIdentifier = System.Security.Claims.ClaimTypes.Name;
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
