using gainshark_api.Mappers.Implementation;
using gainshark_api.Models;
using gainshark_api.MySqlProvider.Contract;
using gainshark_api.Repositories.Contract;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gainshark_api.Repositories.Implementation
{
	public class MuscleGroupRepository : IRepository<MuscleGroup>
	{
		private IMySqlProvider<MuscleGroup> _mySqlProvider;

		public MuscleGroupRepository(IMySqlProvider<MuscleGroup> mySqlProvider)
		{
			_mySqlProvider = mySqlProvider;
		}

		public void AddItem(MuscleGroup muscleGroup)
		{
			string sql = @"	INSERT INTO tbl_musclegroups
							(	MuscleGroup_Name,
								MuscleGroup_Description, 
								MuscleGroup_Image
							)
							VALUES(
								@Name,
								@Description,
								@Image
							)
							ON DUPLICATE KEY UPDATE
								MuscleGroup_Id = MuscleGroup_Id;";

			var engine = _mySqlProvider.GetEngine();

			engine.AddItem(sql, MuscleGroupParameters(muscleGroup));
		}

		public void DeleteItem(int id)
		{
			string sql = @"	DELETE FROM tbl_musclegroups
							WHERE MuscleGroup_Id = @Id;";

			List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
			{
				new MySqlParameter("@Id", id)
			};

			var engine = _mySqlProvider.GetEngine();

			engine.DeleteItem(sql, mySqlParameters);
		}

		public MuscleGroup GetItem(int id)
		{
			string sql = @"	SELECT 	MuscleGroup_Id,
									MuscleGroup_Name,
									MuscleGroup_Description,
									MuscleGroup_Image

							FROM	gainsharksandbox.tbl_musclegroups

							WHERE	MuscleGroup_Id = @Id;";

			List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
			{
				new MySqlParameter("@Id", id)
			};

			var engine = _mySqlProvider.GetEngine();

			return engine.GetItem(sql, mySqlParameters, new MuscleGroupMapper());
		}

		public IList<MuscleGroup> GetItems()
		{
			string sql = @"	SELECT 	MuscleGroup_Id,
									MuscleGroup_Name,
									MuscleGroup_Description,
									MuscleGroup_Image

							FROM	gainsharksandbox.tbl_musclegroups;";

			var engine = _mySqlProvider.GetEngine();

			return engine.GetItems(sql, null, new MuscleGroupMapper());
		}

		public void UpdateItem(MuscleGroup muscleGroup)
		{
			string sql = @"	INSERT INTO tbl_musclegroups
							(	MuscleGroup_Name,
								MuscleGroup_Description, 
								MuscleGroup_Image
							)
							VALUES(
								@Name,
								@Description,
								@Image
							)
							ON DUPLICATE KEY UPDATE
								MuscleGroup_Name = @Name,
								MuscleGroup_Description = @Description,
								MuscleGroup_Image = @Image;";

			var engine = _mySqlProvider.GetEngine();

			engine.UpdateItem(sql, MuscleGroupParameters(muscleGroup));
		}

		private IList<MySqlParameter> MuscleGroupParameters(MuscleGroup muscleGroup)
		{
			List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
			{
				new MySqlParameter("@Id", muscleGroup.Id),
				new MySqlParameter("@Name", muscleGroup.Name),
				new MySqlParameter("@Description", muscleGroup.Description),
				new MySqlParameter("@Image", muscleGroup.Image)
			};

			return mySqlParameters;
		}
	}
}