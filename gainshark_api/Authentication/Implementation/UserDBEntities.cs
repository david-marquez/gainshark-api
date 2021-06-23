using gainshark_api.Authentication.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using gainshark_api.Models;
using gainshark_api.MySqlProvider.Contract;
using gainshark_api.Mappers.Implementation;

namespace gainshark_api.Authentication.Implementation
{
	public class UserDBEntities : IUserDBEntities
	{
		//private IMySqlProvider<User> _mySqlProvider;
		private IMySqlProvider<User> _mySqlProvider = new MySqlProvider.Implementation.MySqlProvider<User>();

		/*public UserDBEntities(IMySqlProvider<User> mySqlProvider)
		{
			_mySqlProvider = mySqlProvider;
		}*/

		public IList<User> Users()
		{
			string sql = @"	SELECT	user.User_Id,
									null as User_FirstName,
									null as User_LastName,
									user.User_UserName,
									null as User_Email,
									user.User_Password,
									null as Role_Id,
									null as Role_Name,
									null as Role_Description

							FROM	tbl_users user";

			var engine = _mySqlProvider.GetEngine();

			return engine.GetItems(sql, null, new UserMapper());
		}
	}
}