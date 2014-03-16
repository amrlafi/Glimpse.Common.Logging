using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Glimpse.Common.Logging.Examples.Mvc4.Net45
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
