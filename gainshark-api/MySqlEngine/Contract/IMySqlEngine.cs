using gainshark_api.Mappers.Contract;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gainshark_api.MySqlEngine.Contract
{
	public interface IMySqlEngine<T> where T : class
	{
		void AddItem(string query, IList<MySqlParameter> mySqlParameters);
		void DeleteItem(string query, IList<MySqlParameter> mySqlParameters);
		T GetItem(string query, IList<MySqlParameter> mySqlParameters, IMapper<T> mapper);
		IList<T> GetItems(string query, IList<MySqlParameter> mySqlParameters, IMapper<T> mapper);
		void UpdateItem(string query, IList<MySqlParameter> mySqlParameters);
		void SetDb(string targetDb);
	}
}
