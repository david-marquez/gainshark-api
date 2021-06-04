using gainshark_api.DataAccess.Contract;
using gainshark_api.Mappers.Contract;
using gainshark_api.Mappers.Implementation;
using gainshark_api.Models;
using gainshark_api.MySqlProvider.Contract;
using gainshark_api.Repositories.Contract;
using gainshark_api.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;

namespace gainshark_api.Unity
{
	public class TypeRegister
	{
		public IUnityContainer GetContainer()
		{
			var container = new UnityContainer();
			RegisterTypes(container);

			return container;
		}

		private void RegisterTypes(UnityContainer container)
		{
			// Mapper registration
			container.RegisterType<IMapper<Role>,
				RoleMapper>();

			// MySqlProvider registration
			container.RegisterType<IMySqlProvider<Role>, 
				MySqlProvider.Implementation.MySqlProvider<Role>>();

			// Repository registration
			container.RegisterType<IRepository<Role>,
				RoleRepository>();

			// Data access registration
			container.RegisterType<IDataAccess<Role>,
				DataAccess.Implementation.DataAccess<Role>>();
		}
	}
}