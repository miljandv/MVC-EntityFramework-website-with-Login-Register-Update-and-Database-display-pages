using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MVC_test.WebUI.Models;

namespace MVC_test
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(RouteNames.Home, "Home",
                new { controller = "Home", action = "Index" });

            routes.MapRoute(RouteNames.RecordLogin, "Record/Login",
                new { controller = "Record", action = "Login" });


            routes.MapRoute(RouteNames.RecordLoggedIn, "Record/LoggedIn",
                new { controller = "Record", action = "LoggedIn" });

            routes.MapRoute(RouteNames.RecordRegister, "Record/Register",
                new { controller = "Record", action = "Register" });

            routes.MapRoute(RouteNames.RecordUsers, "Record/Users",
                new { controller = "Record", action = "Users" });

            routes.MapRoute(RouteNames.RecordUpdate, "Record/Update/{id}",
                new { controller = "Record", action = "Update" });

            routes.MapRoute(RouteNames.RecordRemove, "Record/Remove/{id}",
                new { controller = "Record", action = "Remove" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
