using gainshark_api.DataAccess.Contract;
using gainshark_api.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gainshark_api.DataAccess.Implementation
{
	public class DataAccess<T> : IDataAccess<T> where T : class
	{
		private IRepository<T> _repository;

		public DataAccess(IRepository<T> repository)
		{
			_repository = repository;
		}

		public void AddItem(T item)
		{
			_repository.AddItem(item);
		}

		public void DeleteItem(int id)
		{
			_repository.DeleteItem(id);
		}

		public T GetItem(int id)
		{
			return _repository.GetItem(id);
		}

		public IList<T> GetItems()
		{
			return _repository.GetItems();
		}

		public void UpdateItem(T item)
		{
			_repository.UpdateItem(item);
		}
	}
}