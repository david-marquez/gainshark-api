using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gainshark_api.Mappers.Contract
{
	public interface IMapper<T> where T : class
	{
		T Map(MySqlDataReader dataReader);
	}
}
