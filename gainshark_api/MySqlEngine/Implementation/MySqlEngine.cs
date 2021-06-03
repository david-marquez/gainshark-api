using gainshark_api.Mappers.Contract;
using gainshark_api.MySqlEngine.Contract;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace gainshark_api.MySqlEngine.Implementation
{
	public class MySqlEngine<T> : IMySqlEngine<T> where T : class
	{
		private string _connectionString = ConfigurationManager.ConnectionStrings["MySql"].ConnectionString;
		private string _targetDb = "gainshark";

		public void AddItem(string query, IList<MySqlParameter> mySqlParameters)
		{
			throw new NotImplementedException();
		}

		public void DeleteItem(string query, IList<MySqlParameter> mySqlParameters)
		{
			throw new NotImplementedException();
		}

		public T GetItem(string query, IList<MySqlParameter> mySqlParameters, IMapper<T> mapper)
		{
			throw new NotImplementedException();
		}

		public IList<T> GetItems(string query, IList<MySqlParameter> mySqlParameters, IMapper<T> mapper)
		{
			throw new NotImplementedException();
		}

		public void UpdateItem(string query, IList<MySqlParameter> mySqlParameters)
		{
			throw new NotImplementedException();
		}

		public void SetDb(string targetDb)
		{
			_targetDb = targetDb;
		}
	}
}