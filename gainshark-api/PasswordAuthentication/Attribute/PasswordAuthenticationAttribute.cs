using gainshark_api.PasswordAuthentication.Contract;
using gainshark_api.PasswordAuthentication.Implementation;
using gainshark_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace gainshark_api.PasswordAuthentication.Attribute
{
	public class PasswordAuthenticationAttribute : AuthorizationFilterAttribute
	{
		private IUserSecurity _userSecurity = new UserSecurity();

		public override void OnAuthorization(HttpActionContext actionContext)
		{
			if(actionContext.Request.Headers.Authorization == null)
			{
				actionContext.Response = actionContext.Request
					.CreateResponse(HttpStatusCode.Unauthorized);
			}
			else
			{
				string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
				string decodedToken = Encoding.UTF8.GetString(
					Convert.FromBase64String(authenticationToken));

				string[] credentialsArray = decodedToken.Split(':');

				string userName = credentialsArray[0];
				string password = credentialsArray[1];

				if(_userSecurity.Login(userName, password))
				{
					Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(userName), null);
				}
				else
				{
					actionContext.Response = actionContext.Request
					.CreateResponse(HttpStatusCode.Unauthorized);
				}
			}
		}
	}
}