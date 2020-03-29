using System.Text.RegularExpressions;
using System.Web;
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
              defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional }
          );

            routes.MapRoute(
               name: "ProductCategory",
               url: "cua-hang/danh-muc/{categoryId}/{url}",
               defaults: new { controller = "Product", action = "Category", id= UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "Product",
               url: "cua-hang/san-pham/{productId}/{url}",
               defaults: new { controller = "Product", action = "Details", id = UrlParameter.Optional }
           );


            routes.MapRoute(
               name: "ManageIndex",
               url: "tai-khoan",
               defaults: new { controller = "Manage", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "Manage",
                url: "tai-khoan/{url}",
                defaults: new { controller = "Manage", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            

        }

    }
}
