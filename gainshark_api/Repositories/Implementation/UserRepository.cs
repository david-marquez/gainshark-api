using gainshark_api.Models;
using gainshark_api.MySqlProvider.Contract;
using gainshark_api.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gainshark_api.Repositories.Implementation
{
	public class UserRepository : IRepository<User>
	{
		private IMySqlProvider<User> _userMySqlProvider;

		public UserRepository(IMySqlProvider<User> userMySqlProvider)
		{
			_userMySqlProvider = userMySqlProvider;
		}

		public void AddItem(User item)
		{
			throw new NotImplementedException();
		}

		public void DeleteItem(int id)
		{
			throw new NotImplementedException();
		}

		public User GetItem(int id)
		{
			throw new NotImplementedException();
		}

		public IList<User> GetItems()
		{
			throw new NotImplementedException();
		}

		public void UpdateItem(User item)
		{
			throw new NotImplementedException();
		}
	}
}