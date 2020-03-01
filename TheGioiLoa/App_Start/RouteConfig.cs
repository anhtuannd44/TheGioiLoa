using System.Web.Mvc;
using System.Web.Routing;

namespace TheGioiLoa
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "AllProductCategory",
               url: "cua-hang",
               defaults: new { controller = "Product", action = "Category" }
           );
            routes.MapRoute(
               name: "Product",
               url: "san-pham/{Url}-{productId}",
               defaults: new { controller = "Product", action = "Details", id = UrlParameter.Optional }
           );

            

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}
