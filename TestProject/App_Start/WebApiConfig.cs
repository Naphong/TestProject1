using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Routing;

namespace TestProject
{
    public static class WebApiConfig
    {[HttpPost]
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
               name: "DefaultApi2",
               routeTemplate: "api/{controller}/{action}/{id}/{id2}",
               defaults: new { id = RouteParameter.Optional,id2=RouteParameter.Optional }
           );
        }
    }
}
