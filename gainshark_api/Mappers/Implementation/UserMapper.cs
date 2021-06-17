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

			user.Id = Convert.ToInt32(dataReader[0]);
			user.FirstName = dataReader[1] as string ?? null;
			user.LastName = dataReader[2] as string ?? null;
			user.UserName = dataReader[3] as string ?? null;
			user.Email = dataReader[4] as string ?? null;
			user.Password = dataReader[5] as string ?? null;

			user.Role.Id = Convert.ToInt32(dataReader[6]);
			user.Role.Name = dataReader[7] as string ?? null;
			user.Role.Description = dataReader[8] as string ?? null;

			return user;
		}
	}
}