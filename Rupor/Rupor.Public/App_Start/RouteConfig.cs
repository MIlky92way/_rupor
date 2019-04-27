using System.Web.Mvc;
using System.Web.Routing;

namespace Rupor.Public
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "without_index",
            //    url: "{controller}/{id}",
            //    defaults: new { controller = "Category", action = "Index", id = UrlParameter.Optional }
            //);
            //routes.MapRoute(
            //name: "pictures",
            //url: "pic/{action}/{id}",
            //defaults: new { controller = "Pic", action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
