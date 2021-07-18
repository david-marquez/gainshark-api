using System;
using System.Threading.Tasks;
using System.Web.Http;
using gainshark_api.TokenAuthorization;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(gainshark_api.OwinStartup))]

namespace gainshark_api
{
	public class OwinStartup
	{
		public void Configuration(IAppBuilder app)
		{
			app.UseCors(CorsOptions.AllowAll);

			OAuthAuthorizationServerOptions oAuthOptions = new OAuthAuthorizationServerOptions()
			{
				AllowInsecureHttp = true,

				// Token generation path
				TokenEndpointPath = new PathString("/api/token"),

				// Set token expiration time -- currently 2 hours
				AccessTokenExpireTimeSpan = TimeSpan.FromHours(2),

				// TokenAuthorizationServerProvider class to validate credentials
				Provider = new TokenAuthorizationServerProvider()
			};

			// Token generation
			app.UseOAuthAuthorizationServer(oAuthOptions);
			app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

			HttpConfiguration config = new HttpConfiguration();
			WebApiConfig.Register(config);

			// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
		}
	}
}
