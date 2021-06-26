using gainshark_api.Authentication.Contract;
using gainshark_api.Authentication.Implementation;
using gainshark_api.DataAccess.Contract;
using gainshark_api.Encryption.Contract;
using gainshark_api.Encryption.Implementation;
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
			container.RegisterType<IMapper<Program>,
				ProgramMapper>();
			container.RegisterType<IMapper<User>,
				UserMapper>();

			// MySqlProvider registration
			container.RegisterType<IMySqlProvider<Role>, 
				MySqlProvider.Implementation.MySqlProvider<Role>>();
			container.RegisterType<IMySqlProvider<MuscleGroup>,
				MySqlProvider.Implementation.MySqlProvider<MuscleGroup>>();
			container.RegisterType<IMySqlProvider<Exercise>,
				MySqlProvider.Implementation.MySqlProvider<Exercise>>();
			container.RegisterType<IMySqlProvider<Program>,
				MySqlProvider.Implementation.MySqlProvider<Program>>();
			container.RegisterType <IMySqlProvider<User>,
				MySqlProvider.Implementation.MySqlProvider<User>>();

			// Repository registration
			container.RegisterType<IRepository<Role>,
				RoleRepository>();
			container.RegisterType<IRepository<MuscleGroup>,
				MuscleGroupRepository>();
			container.RegisterType<IRepository<Exercise>,
				ExerciseRepository>();
			container.RegisterType<IRepository<Program>,
				ProgramRepository>();
			container.RegisterType<IRepository<User>,
				UserRepository>();

			// Data access registration
			container.RegisterType<IDataAccess<Role>,
				DataAccess.Implementation.DataAccess<Role>>();
			container.RegisterType<IDataAccess<MuscleGroup>,
				DataAccess.Implementation.DataAccess<MuscleGroup>>();
			container.RegisterType<IDataAccess<Exercise>,
				DataAccess.Implementation.DataAccess<Exercise>>();
			container.RegisterType<IDataAccess<Program>,
				DataAccess.Implementation.DataAccess<Program>>();
			container.RegisterType<IDataAccess<User>,
				DataAccess.Implementation.DataAccess<User>>();

			// Authentication registration
			container.RegisterType<IUserDBEntities,
				UserDBEntities>();
			container.RegisterType<IUserSecurity,
				UserSecurity>();

			// Encryption registration
			container.RegisterType<IBCryptEncryption,
				BCryptEncryption>();
		}
	}
}