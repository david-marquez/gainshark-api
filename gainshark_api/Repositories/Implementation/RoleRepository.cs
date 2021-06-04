using gainshark_api.Mappers.Implementation;
using gainshark_api.Models;
using gainshark_api.MySqlProvider.Contract;
using gainshark_api.Repositories.Contract;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gainshark_api.Repositories.Implementation
{
	public class RoleRepository : IRepository<Role>
	{
		private IMySqlProvider<Role> _mySqlProvider;

		public RoleRepository(IMySqlProvider<Role> mySqlProvider)
		{
			_mySqlProvider = mySqlProvider;
		}

		public void AddItem(Role role)
		{
			string sql = @"";

			var engine = _mySqlProvider.GetEngine();
			engine.SetDb("gainsharksandbox");

			engine.AddItem(sql, GetMySqlParameters(role));
		}

		public void DeleteItem(int id)
		{
			string sql = @"";

			List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
			{
				new MySqlParameter("@Id", id)
			};

			var engine = _mySqlProvider.GetEngine();
			engine.SetDb("gainsharksandbox");

			engine.DeleteItem(sql, mySqlParameters);
		}

		public Role GetItem(int id)
		{
			string sql = @"	SELECT 	Role_Id,
									Role_Name,
									Role_Description
        
							FROM 	tbl_roles

							WHERE 	Role_Id = @Id;";

			List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
			{
				new MySqlParameter("@Id", id)
			};

			var engine = _mySqlProvider.GetEngine();
			engine.SetDb("gainsharksandbox");

			return engine.GetItem(sql, mySqlParameters, new RoleMapper());
		}

		public IList<Role> GetItems()
		{
			string sql = @"	SELECT 	Role_Id,
									Role_Name,
									Role_Description
        
							FROM 	tbl_roles;";

			var engine = _mySqlProvider.GetEngine();
			engine.SetDb("gainsharksandbox");

			return engine.GetItems(sql, null, new RoleMapper());
		}

		public void UpdateItem(Role role)
		{
			string sql = @"";

			var engine = _mySqlProvider.GetEngine();
			engine.SetDb("gainsharksandbox");

			engine.UpdateItem(sql, GetMySqlParameters(role));
		}

		private IList<MySqlParameter> GetMySqlParameters(Role role)
		{
			List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
			{
				new MySqlParameter("@Id", role.Id),
				new MySqlParameter("@Name", role.Name),
				new MySqlParameter("@Description", role.Description)
			};

			return mySqlParameters;
		}
	}
}