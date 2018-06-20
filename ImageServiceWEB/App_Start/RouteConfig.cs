/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex4
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ImageServiceWEB
{
    /// <summary>
    /// Class RouteConfig.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// The function registeing the routes that are given as
        /// arguments.
        /// </summary>
        /// <param name="routes">collection of rountes</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "ImageWeb", action = "Index" }
            );

            routes.MapRoute(
                name: "Secondary",
                url: "{controller}/{action}",
                defaults: new { controller = "ImageWeb", action = "Index" }
            );
        }
    }
}
