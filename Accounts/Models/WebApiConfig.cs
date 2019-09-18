using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;


namespace Accounts.Common
{
    public static class WebApiConfig
    {
        public static string UrlPrefix { get { return "api"; } }
        public static string UrlPrefixRelative { get { return "~/api"; } }

        public static void Register(RouteCollection routes)
        {
            routes.MapHttpRoute(
               name: "DefaultApi",
               routeTemplate: WebApiConfig.UrlPrefix + "/{controller}/{action}/{id}",
              defaults: new { id = RouteParameter.Optional }

           );
        }
    }
}