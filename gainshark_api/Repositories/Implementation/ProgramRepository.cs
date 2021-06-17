using gainshark_api.Models;
using gainshark_api.MySqlProvider.Contract;
using gainshark_api.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gainshark_api.Repositories.Implementation
{
	public class ProgramRepository : IRepository<Program>
	{
		private IMySqlProvider<Program> _programMySqlProvider;

		public ProgramRepository(IMySqlProvider<Program> programMySqlProvider)
		{
			_programMySqlProvider = programMySqlProvider;
		}

		public void AddItem(Program item)
		{
			throw new NotImplementedException();
		}

		public void DeleteItem(int id)
		{
			throw new NotImplementedException();
		}

		public Program GetItem(int id)
		{
			throw new NotImplementedException();
		}

		public IList<Program> GetItems()
		{
			throw new NotImplementedException();
		}

		public void UpdateItem(Program item)
		{
			throw new NotImplementedException();
		}
	}
}