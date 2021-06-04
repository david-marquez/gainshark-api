using gainshark_api.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace gainshark_api
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services
			var container = new TypeRegister().GetContainer();
			config.DependencyResolver = new UnityResolver(container);

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
