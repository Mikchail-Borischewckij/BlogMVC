using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Blogs.WebUI.Infrastructure;
using WebMatrix.WebData;
using System.Web.Security;

namespace Blogs.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            RouteConfig.RegisterRoutes(RouteTable.Routes);

            WebSecurity.InitializeDatabaseConnection("BlogDataBase", "Users", "UserId", "Login", true);
            if (!Roles.RoleExists("fan")) Roles.CreateRole("fan");
            if (!Roles.RoleExists("administrator")) Roles.CreateRole("administrator");
            if (!Roles.RoleExists("manager")) Roles.CreateRole("manager");
      
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(null,
                "{controller}/{action}", new { controller = "Post", action = "ListPost" });
        }
    }
}