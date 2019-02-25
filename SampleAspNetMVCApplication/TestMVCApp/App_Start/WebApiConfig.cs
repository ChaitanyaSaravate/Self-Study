using System.Web.Http;

namespace RestaurantServices
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			config.Routes.MapHttpRoute(
				name: "ActionRoute",
				routeTemplate: "api/{controller}/{action}/{city}",
				defaults: new { city = RouteParameter.Optional }
			);
		}
	}
}
