using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TestMVCApp
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(name: "StudentList",
				url: "students",
				defaults: new { controller="students", action="ListAll" });

			routes.MapRoute(name: "StudentSearch",
				url: "students/{action}/{id}",
				defaults: new { controller="students", action="search", id = UrlParameter.Optional });

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Restaurant", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
