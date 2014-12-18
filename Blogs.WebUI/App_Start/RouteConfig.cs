using System.Web.Mvc;
using System.Web.Routing;

namespace Blogs.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: null,
                url: "Post/AllPost/{idPost}",
                defaults: new {Controller = "Post", Action = "AllPost"}
                );

            routes.MapRoute(
                name: "MyRoute",
                url: "Post/page{page}",
                defaults: new {Controller = "Post", Action = "ListPost"}
                );

            routes.MapRoute(
                name: null,
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Post", action = "ListPost", id = UrlParameter.Optional}
                );

        }
    }
}
