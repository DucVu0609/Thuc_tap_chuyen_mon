using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Webanvexemphim
{
    public class RouteConfig
    {
        
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
         name: "Checkout",
         url: "thanh-toan",
         defaults: new { controller = "Checkout", action = "Thanh_toan", id = UrlParameter.Optional }
     );
            routes.MapRoute(
            name: "mua-ve",
            url: "Mua-ve",
            defaults: new { controller = "Cart", action = "Additem", id = UrlParameter.Optional }
        );
            routes.MapRoute(
         name: "gio-hang",
         url: "gio-hang",
         defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional }
     );
            routes.MapRoute(
           name: "post-detail",
           url: "Chi-tiet-vai-viet/{slug}",
           defaults: new { controller = "PostC", action = "postDetaail", id = UrlParameter.Optional }
       );
            routes.MapRoute(
          name: "post-category",
          url: "loai-bai-viet/{slug}",
          defaults: new { controller = "PostC", action = "post_cate", id = UrlParameter.Optional }
      );
            routes.MapRoute(
              name: "movie-category",
              url: "loai-phim/{slug}",
              defaults: new { controller = "Movies", action = "Movie_Category", id = UrlParameter.Optional }
          );
            routes.MapRoute(
                name: "chi tiet movies",
                url: "chi-tiet-movie/{slug}",
                defaults: new { controller = "Movies", action = "MoviesDetail", id = UrlParameter.Optional }
            );
            routes.MapRoute(
              name: "Lien-he",
              url: "lien-he",
              defaults: new { controller = "Home", action = "LienHe", id = UrlParameter.Optional }
          );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
