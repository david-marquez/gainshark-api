using gainshark_api.Mappers.Contract;
using gainshark_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace gainshark_api.Mappers.Implementation
{
	public class RoleMapper : IMapper<Role>
	{
		public Role Map(MySqlDataReader dataReader)
		{
			Role role = new Role();

			role.Id = Convert.ToInt32(dataReader[0]);
			role.Name = dataReader[1] as string ?? null;
			role.Description = dataReader[2] as string ?? null;

			return role;
		}
	}
}