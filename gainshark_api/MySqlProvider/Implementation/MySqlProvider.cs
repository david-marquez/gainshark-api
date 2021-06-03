using gainshark_api.MySqlProvider.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using gainshark_api.MySqlEngine.Contract;

namespace gainshark_api.MySqlProvider.Implementation
{
	public class MySqlProvider<T> : IMySqlProvider<T> where T : class
	{
		public IMySqlEngine<T> GetEngine()
		{
			return new MySqlEngine.Implementation.MySqlEngine<T>();
		}
	}
}