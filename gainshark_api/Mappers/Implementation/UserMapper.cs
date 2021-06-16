using gainshark_api.Mappers.Contract;
using gainshark_api.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gainshark_api.Mappers.Implementation
{
	public class UserMapper : IMapper<User>
	{
		public User Map(MySqlDataReader dataReader)
		{
			User user = new User();
			Role role = new Role();

			user.Id = (int)dataReader[0];
			user.FirstName = dataReader[1] as string ?? null;
			user.LastName = dataReader[2] as string ?? null;
			user.UserName = dataReader[3] as string ?? null;
			user.Password = dataReader[4] as string ?? null;

			role.Id = (int)dataReader[5];
			role.Name = dataReader[6] as string ?? null;
			role.Description = dataReader[7] as string ?? null;

			user.Role = role;

			return user;
		}
	}
}