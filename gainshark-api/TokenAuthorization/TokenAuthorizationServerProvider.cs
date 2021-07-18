using gainshark_api.Models;
using gainshark_api.PasswordAuthentication.Implementation;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace gainshark_api.TokenAuthorization
{
	public class TokenAuthorizationServerProvider : OAuthAuthorizationServerProvider
	{
		public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
		{
			context.Validated();
		}

		public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
		{
			string decodedPassword = Encoding.UTF8.GetString(
					Convert.FromBase64String(context.Password));

			PasswordAuthenticationRepository userRepo = new PasswordAuthenticationRepository();

			User user = userRepo.AuthenticateUser(context.UserName, decodedPassword);

			if(user == null)
			{
				context.SetError("invalid_grant", "Provided username or password is incorrect");
				return;
			}
			
			var identity = new ClaimsIdentity(context.Options.AuthenticationType);
			identity.AddClaim(new Claim(ClaimTypes.Role, user.Role.Name));
			identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
			identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

			context.Validated(identity);
		}
	}
}