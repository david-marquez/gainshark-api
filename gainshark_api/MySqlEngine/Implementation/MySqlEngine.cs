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
		private string _targetDb = "gainsharksandbox";

		public void AddItem(string query, IList<MySqlParameter> mySqlParameters)
		{
			using(MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();

				using(MySqlCommand command = new MySqlCommand(query, connection))
				{
					command.Connection.ChangeDatabase(_targetDb);

					if(mySqlParameters != null)
					{
						foreach(MySqlParameter mySqlParameter in mySqlParameters)
						{
							command.Parameters.Add(mySqlParameter);
						}
					}

					command.ExecuteNonQuery();
				}
			}
		}

		public void DeleteItem(string query, IList<MySqlParameter> mySqlParameters)
		{
			using(MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();

				using(MySqlCommand command = new MySqlCommand(query, connection))
				{
					command.Connection.ChangeDatabase(_targetDb);

					if(mySqlParameters != null)
					{
						foreach(MySqlParameter mySqlParameter in mySqlParameters)
						{
							command.Parameters.Add(mySqlParameter);
						}
					}

					command.ExecuteNonQuery();
				}
			}
		}

		public T GetItem(string query, IList<MySqlParameter> mySqlParameters, IMapper<T> mapper)
		{
			T item = Activator.CreateInstance<T>();

			using(MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();

				using(MySqlCommand command = new MySqlCommand(query, connection))
				{
					command.Connection.ChangeDatabase(_targetDb);

					if(mySqlParameters != null)
					{
						foreach(MySqlParameter mySqlParameter in mySqlParameters)
						{
							command.Parameters.Add(mySqlParameter);
						}
					}

					using(MySqlDataReader dataReader = command.ExecuteReader())
					{
						if(dataReader.Read())
						{
							item = mapper.Map(dataReader);
						}
					}
				}
			}

			return item;
		}

		public IList<T> GetItems(string query, IList<MySqlParameter> mySqlParameters, IMapper<T> mapper)
		{
			List<T> items = new List<T>();

			using(MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();

				using(MySqlCommand command = new MySqlCommand(query, connection))
				{
					command.Connection.ChangeDatabase(_targetDb);

					if(mySqlParameters != null)
					{
						foreach(MySqlParameter mySqlParameter in mySqlParameters)
						{
							command.Parameters.Add(mySqlParameter);
						}
					}

					using(MySqlDataReader dataReader = command.ExecuteReader())
					{
						while(dataReader.Read())
						{
							items.Add(mapper.Map(dataReader));
						}
					}
				}
			}

			return items;
		}

		public void UpdateItem(string query, IList<MySqlParameter> mySqlParameters)
		{
			using(MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();

				using(MySqlCommand command = new MySqlCommand(query, connection))
				{
					command.Connection.ChangeDatabase(_targetDb);

					if(mySqlParameters != null)
					{
						foreach(MySqlParameter mySqlParameter in mySqlParameters)
						{
							command.Parameters.Add(mySqlParameter);
						}
					}

					command.ExecuteNonQuery();
				}
			}
		}

		public void SetDb(string targetDb)
		{
			_targetDb = targetDb;
		}
	}
}