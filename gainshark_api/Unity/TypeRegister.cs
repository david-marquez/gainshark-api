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
			container.RegisterType<IMapper<MuscleGroup>, 
				MuscleGroupMapper>();
			container.RegisterType<IMapper<Exercise>,
				ExerciseMapper>();

			// MySqlProvider registration
			container.RegisterType<IMySqlProvider<Role>, 
				MySqlProvider.Implementation.MySqlProvider<Role>>();
			container.RegisterType<IMySqlProvider<MuscleGroup>,
				MySqlProvider.Implementation.MySqlProvider<MuscleGroup>>();
			container.RegisterType<IMySqlProvider<Exercise>,
				MySqlProvider.Implementation.MySqlProvider<Exercise>>();

			// Repository registration
			container.RegisterType<IRepository<Role>,
				RoleRepository>();
			container.RegisterType<IRepository<MuscleGroup>,
				MuscleGroupRepository>();
			container.RegisterType<IRepository<Exercise>,
				ExerciseRepository>();

			// Data access registration
			container.RegisterType<IDataAccess<Role>,
				DataAccess.Implementation.DataAccess<Role>>();
			container.RegisterType<IDataAccess<MuscleGroup>,
				DataAccess.Implementation.DataAccess<MuscleGroup>>();
			container.RegisterType<IDataAccess<Exercise>,
				DataAccess.Implementation.DataAccess<Exercise>>();
		}
	}
}