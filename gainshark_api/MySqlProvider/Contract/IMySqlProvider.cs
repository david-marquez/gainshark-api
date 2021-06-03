using gainshark_api.MySqlEngine.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gainshark_api.MySqlProvider.Contract
{
	public interface IMySqlProvider<T> where T : class
	{
		IMySqlEngine<T> GetEngine();
	}
}
