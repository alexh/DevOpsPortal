using System.Web.Mvc;
using System.Web.Routing;

namespace DevOpsPortal {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Dashboard", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                    name: "404-PageNotFound",
                    url: "{*url}",
                    defaults: new { controller = "Home", action = "404" }
    );
        }
    }
}