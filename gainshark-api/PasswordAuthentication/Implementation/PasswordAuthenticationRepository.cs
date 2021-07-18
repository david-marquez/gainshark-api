using gainshark_api.Encryption.Contract;
using gainshark_api.Encryption.Implementation;
using gainshark_api.Models;
using gainshark_api.PasswordAuthentication.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gainshark_api.PasswordAuthentication.Implementation
{
	public class PasswordAuthenticationRepository
	{
		private IUserDBEntities _userDBEntities = new UserDBEntities();
		private IBCryptEncryption _bCryptEncryption = new BCryptEncryption();

		public User AuthenticateUser(string username, string password)
		{
			List<User> users = (List<User>)_userDBEntities.Users();

			return users.FirstOrDefault(user =>
				user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
				&& _bCryptEncryption.Verify(password, user.Password));
		}
	}
}