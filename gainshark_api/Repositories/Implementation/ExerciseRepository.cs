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
	public class ExerciseRepository : IRepository<Exercise>
	{
		private IMySqlProvider<Exercise> _exerciseMySqlProvider;
		private IMySqlProvider<MuscleGroup> _muscleGroupMySqlProvider;

		public ExerciseRepository(IMySqlProvider<Exercise> exerciseMySqlProvider,
			IMySqlProvider<MuscleGroup> muscleGroupMySqlProvider)
		{
			_exerciseMySqlProvider = exerciseMySqlProvider;
			_muscleGroupMySqlProvider = muscleGroupMySqlProvider;
		}

		public void AddItem(Exercise exercise)
		{
			string exerciseSql = @"	INSERT INTO tbl_exercises
									(	Exercise_Name, 
										Exercise_Description, 
										Exercise_Image
									)
									VALUES(
										@Exercise_Name,
										@Exercise_Description,
										@Exercise_Image
									)
									ON DUPLICATE KEY UPDATE
										Exercise_Id = Exercise_Id;";

			string muscleGroupSql = @"	INSERT INTO rltn_exercise_musclegroup
										(	Exercise_Id,
											MuscleGroup_Id
										)
										VALUES(
											(	SELECT	Exercise_Id
												FROM	tbl_Exercises
												WHERE	Exercise_Name = @Exercise_Name),
											@MuscleGroup_Id
										);";

			var exerciseEngine = _exerciseMySqlProvider.GetEngine();
			var muscleGroupEngine = _muscleGroupMySqlProvider.GetEngine();

			List<MySqlParameter> exerciseParameters = (List<MySqlParameter>)ExerciseParameters(exercise);
			exerciseEngine.AddItem(exerciseSql, exerciseParameters);

			foreach(MuscleGroup muscleGroup in exercise.MuscleGroups)
			{
				List<MySqlParameter> muscleGroupParameters = (List<MySqlParameter>)MuscleGroupParameters(muscleGroup);
				muscleGroupParameters.Add(new MySqlParameter("@Exercise_Name", exercise.Name));

				muscleGroupEngine.AddItem(muscleGroupSql, muscleGroupParameters);
			}
		}

		public void DeleteItem(int id)
		{
			throw new NotImplementedException();
		}

		public Exercise GetItem(int id)
		{
			string exerciseSql = @"	SELECT	exercise.Exercise_Id,
											exercise.Exercise_Name,
											exercise.Exercise_Description,
											exercise.Exercise_Image,
											0 as Reps,
											0 as Sets,
											0 as Duration,
											0 as Weight
									
									FROM	tbl_exercises exercise

									WHERE	exercise.Exercise_Id = @Id";

			string muscleGroupSql = @"	SELECT	muscleGroup.MuscleGroup_Id,
												muscleGroup.MuscleGroup_Name,
												muscleGroup.MuscleGroup_Description,
												muscleGroup.MuscleGroup_Image

										FROM	tbl_musclegroups muscleGroup, rltn_exercise_musclegroup rltn

										WHERE	rltn.MuscleGroup_Id = muscleGroup.MuscleGroup_Id
										AND		rltn.Exercise_Id = @Id;";

			List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
			{
				new MySqlParameter("@Id", id)
			};

			var exerciseEngine = _exerciseMySqlProvider.GetEngine();
			var muscleGroupEngine = _muscleGroupMySqlProvider.GetEngine();

			Exercise exercise = exerciseEngine.GetItem(exerciseSql, mySqlParameters, new ExerciseMapper());
			exercise.MuscleGroups = muscleGroupEngine.GetItems(muscleGroupSql, mySqlParameters, new MuscleGroupMapper());

			return exercise;
		}

		public IList<Exercise> GetItems()
		{
			string exerciseSql = @"	SELECT	exercise.Exercise_Id,
											exercise.Exercise_Name,
											exercise.Exercise_Description,
											exercise.Exercise_Image,
											0 as Reps,
											0 as Sets,
											0 as Duration,
											0 as Weight
									
									FROM	tbl_exercises exercise;";

			string muscleGroupSql = @"	SELECT	muscleGroup.MuscleGroup_Id,
												muscleGroup.MuscleGroup_Name,
												muscleGroup.MuscleGroup_Description,
												muscleGroup.MuscleGroup_Image

										FROM	tbl_musclegroups muscleGroup, rltn_exercise_musclegroup rltn

										WHERE	rltn.MuscleGroup_Id = muscleGroup.MuscleGroup_Id
										AND		rltn.Exercise_Id = @ExerciseId;";

			var exerciseEngine = _exerciseMySqlProvider.GetEngine();
			var muscleGroupEngine = _muscleGroupMySqlProvider.GetEngine();

			List<Exercise> exercises = (List<Exercise>)exerciseEngine.GetItems(exerciseSql, null, new ExerciseMapper());

			foreach(Exercise exercise in exercises)
			{
				List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
				{
					new MySqlParameter("@ExerciseId", exercise.Id)
				};

				exercise.MuscleGroups = (List<MuscleGroup>)muscleGroupEngine.GetItems(muscleGroupSql, mySqlParameters, new MuscleGroupMapper());
			}

			return exercises;
		}

		public void UpdateItem(Exercise exercise)
		{
			throw new NotImplementedException();
		}

		private IList<MySqlParameter> ExerciseParameters(Exercise exercise)
		{
			List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
			{
				new MySqlParameter("@Exercise_Id", exercise.Id),
				new MySqlParameter("@Exercise_Name", exercise.Name),
				new MySqlParameter("@Exercise_Description", exercise.Description),
				new MySqlParameter("@Exercise_Image", exercise.Image)
			};

			return mySqlParameters;
		}

		private IList<MySqlParameter> MuscleGroupParameters(MuscleGroup muscleGroup)
		{
			List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
			{
				new MySqlParameter("@MuscleGroup_Id", muscleGroup.Id),
				new MySqlParameter("@MuscleGroup_Name", muscleGroup.Name),
				new MySqlParameter("@MuscleGroup_Description", muscleGroup.Description),
				new MySqlParameter("@MuscleGroup_Image", muscleGroup.Image)
			};

			return mySqlParameters;
		}
	}
}