using gainshark_api.PasswordAuthentication.Contract;
using gainshark_api.Encryption.Contract;
using gainshark_api.Encryption.Implementation;
using gainshark_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gainshark_api.PasswordAuthentication.Implementation
{
	public class UserSecurity : IUserSecurity
	{
		private IUserDBEntities _userDBEntities = new UserDBEntities();
		private IBCryptEncryption _bCryptEncryption = new BCryptEncryption();

		public bool Login(string userName, string password)
		{
			List<User> users = (List<User>)_userDBEntities.Users();

			return users.Any(user => 
				user.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase) &&
				_bCryptEncryption.Verify(password, user.Password));
		}
	}
}