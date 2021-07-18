using gainshark_api.PasswordAuthentication.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using gainshark_api.Models;
using gainshark_api.MySqlProvider.Contract;
using gainshark_api.Mappers.Implementation;

namespace gainshark_api.PasswordAuthentication.Implementation
{
	public class UserDBEntities : IUserDBEntities
	{
		private IMySqlProvider<User> _mySqlProvider = new MySqlProvider.Implementation.MySqlProvider<User>();
		

		public IList<User> Users()
		{
			string sql = @"	SELECT	user.User_Id,
									user.User_FirstName,
									user.User_LastName,
									user.User_UserName,
									user.User_Email,
									user.User_Password,
									role.Role_Id,
									role.Role_Name,
									role.Role_Description

							FROM	tbl_users user, tbl_roles role

							WHERE	role.Role_Id = user.User_Role";

			var engine = _mySqlProvider.GetEngine();

			return engine.GetItems(sql, null, new UserMapper());
		}
	}
}