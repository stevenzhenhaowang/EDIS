using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EDISAngular
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {          
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");      
            routes.RouteExistingFiles = true;
            routes.MapMvcAttributeRoutes();//take precedence 

            //routes.MapRoute(
            //    name:null,
            //    url:"admin/{action}"

            //    );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
