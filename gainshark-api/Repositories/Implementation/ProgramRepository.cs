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
	public class ProgramRepository : IRepository<Program>
	{
		private IMySqlProvider<Program> _programMySqlProvider;
		private IMySqlProvider<Exercise> _exerciseMySqlProvider;
		private IMySqlProvider<MuscleGroup> _muscleGroupMySqlProvider;

		public ProgramRepository(IMySqlProvider<Program> programMySqlProvider,
			IMySqlProvider<Exercise> exerciseMySqlProvider,
			IMySqlProvider<MuscleGroup> muscleGroupMySqlProvider)
		{
			_programMySqlProvider = programMySqlProvider;
			_exerciseMySqlProvider = exerciseMySqlProvider;
			_muscleGroupMySqlProvider = muscleGroupMySqlProvider;
		}

		public void AddItem(Program program)
		{
			string programSql = @"	INSERT	INTO tbl_programs
									(	User_Id,
										Program_Name,
										Program_Description,
										Program_DateCreated
									)
									VALUES(
										@User_Id,
										@Program_Name,
										@Program_Description,
										@Program_DateCreated
									);

									/* Delete existing program-exercise relationships */

									DELETE	FROM rltn_program_exercise
									WHERE	Program_Id = @Program_Id;";

			string exerciseSql = @"	/* Insert program-exercise relationships */
									INSERT	INTO rltn_program_exercise
									(	Program_Id,
										Exercise_Id,
										Position,
										Sets,
										Reps,
										Weight,
										Duration
									)
									VALUES(
										(	SELECT	Program_Id
											FROM	tbl_programs
											WHERE	Program_DateCreated = @Program_DateCreated),
										@Exercise_Id,
										@Exercise_Position,
										@Exercise_Sets,
										@Exercise_Reps,
										@Exercise_Weight,
										@Exercise_Duration
									);";

			var programEngine = _programMySqlProvider.GetEngine();
			var exerciseEngine = _exerciseMySqlProvider.GetEngine();

			List<MySqlParameter> programParameters = (List<MySqlParameter>)ProgramParameters(program);
			programEngine.AddItem(programSql, programParameters);

			foreach(Exercise exercise in program.Exercises)
			{
				List<MySqlParameter> exerciseParameters = (List<MySqlParameter>)ExerciseParameters(exercise);
				exerciseParameters.Add(new MySqlParameter("@Program_DateCreated", program.DateCreated));

				exerciseEngine.AddItem(exerciseSql, exerciseParameters);
			}
		}

		public void DeleteItem(int id)
		{
			string sql = @"	/* Delete program-exercise relationships */

							DELETE	FROM rltn_program_exercise
							WHERE	Program_Id =  @Id;

							/* Delete program */

							DELETE	FROM tbl_programs
							WHERE	Program_Id = @Id;";

			List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
			{
				new MySqlParameter("@Id", id)
			};

			var engine = _programMySqlProvider.GetEngine();

			engine.DeleteItem(sql, mySqlParameters);
		}

		public Program GetItem(int id)
		{
			string programSql = @"	SELECT	program.Program_Id,
											program.User_Id,
											program.Program_Name,
											program.Program_Description,
											program.Program_DateCreated

									FROM	tbl_programs program

									WHERE	program.Program_Id = @Program_Id;";

			string exerciseSql = @"	SELECT	exercise.Exercise_Id,
											exercise.Exercise_Name,
											exercise.Exercise_Description,
											exercise.Exercise_Image,
											rltn.Position,
											rltn.Reps,
											rltn.Sets,
											rltn.Duration,
											rltn.Weight
									
									FROM	tbl_exercises exercise, rltn_program_exercise rltn

									WHERE	rltn.Exercise_Id = exercise.Exercise_Id
									AND		rltn.Program_Id = @Program_Id

									ORDER	BY rltn.Position;";

			string muscleGroupSql = @"	SELECT	muscleGroup.MuscleGroup_Id,
												muscleGroup.MuscleGroup_Name,
												muscleGroup.MuscleGroup_Description,
												muscleGroup.MuscleGroup_Image

										FROM	tbl_musclegroups muscleGroup, rltn_exercise_musclegroup rltn

										WHERE	rltn.MuscleGroup_Id = muscleGroup.MuscleGroup_Id
										AND		rltn.Exercise_Id = @Exercise_Id;";

			List<MySqlParameter> programParameters = new List<MySqlParameter>()
			{
				new MySqlParameter("@Program_Id", id)
			};

			var programEngine = _programMySqlProvider.GetEngine();
			var exerciseEngine = _exerciseMySqlProvider.GetEngine();
			var muscleGroupEngine = _muscleGroupMySqlProvider.GetEngine();

			Program program = programEngine.GetItem(programSql,
													programParameters,
													new ProgramMapper());

			program.Exercises = (List<Exercise>)exerciseEngine.GetItems(exerciseSql,
																		programParameters,
																		new ExerciseMapper());

			foreach(Exercise exercise in program.Exercises)
			{
				List<MySqlParameter> muscleGroupParameters = new List<MySqlParameter>()
				{
					new MySqlParameter("@Exercise_Id", exercise.Id)
				};

				exercise.MuscleGroups = (List<MuscleGroup>)muscleGroupEngine.GetItems(muscleGroupSql,
																						muscleGroupParameters,
																						new MuscleGroupMapper());
			}

			return program;
		}

		public IList<Program> GetItems()
		{
			string programSql = @"	SELECT	program.Program_Id,
											program.User_Id,
											program.Program_Name,
											program.Program_Description,
											program.Program_DateCreated

									FROM	tbl_programs program;";

			string exerciseSql = @"	SELECT	exercise.Exercise_Id,
											exercise.Exercise_Name,
											exercise.Exercise_Description,
											exercise.Exercise_Image,
											0 as Position,
											0 as Reps,
											0 as Sets,
											0 as Duration,
											0 as Weight
									
									FROM	tbl_exercises exercise, rltn_program_exercise rltn

									WHERE	rltn.Exercise_Id = exercise.Exercise_Id
									AND		rltn.Program_Id = @Program_Id;";

			string muscleGroupSql = @"	SELECT	muscleGroup.MuscleGroup_Id,
												muscleGroup.MuscleGroup_Name,
												muscleGroup.MuscleGroup_Description,
												muscleGroup.MuscleGroup_Image

										FROM	tbl_musclegroups muscleGroup, rltn_exercise_musclegroup rltn

										WHERE	rltn.MuscleGroup_Id = muscleGroup.MuscleGroup_Id
										AND		rltn.Exercise_Id = @Exercse_Id;";

			var programEngine = _programMySqlProvider.GetEngine();
			var exerciseEngine = _exerciseMySqlProvider.GetEngine();
			var muscleGroupEngine = _muscleGroupMySqlProvider.GetEngine();

			List<Program> programs = (List<Program>)programEngine.GetItems(programSql, 
																			null, 
																			new ProgramMapper());

			foreach(Program program in programs)
			{
				List<MySqlParameter> exerciseParameters = new List<MySqlParameter>()
				{
					new MySqlParameter("@Program_Id", program.Id)
				};

				program.Exercises = (List<Exercise>)exerciseEngine.GetItems(exerciseSql, 
																			exerciseParameters, 
																			new ExerciseMapper());

				foreach(Exercise exercise in program.Exercises)
				{
					List<MySqlParameter> muscleGroupParameters = new List<MySqlParameter>()
					{
						new MySqlParameter("@Exercse_Id", exercise.Id)
					};

					exercise.MuscleGroups = (List<MuscleGroup>)muscleGroupEngine.GetItems(muscleGroupSql, 
																							muscleGroupParameters, 
																							new MuscleGroupMapper());
				}
			}

			return programs;
		}

		public void UpdateItem(Program program)
		{
			string programSql = @"	UPDATE	tbl_programs
									
									SET	Program_Name = @Program_Name,
										Program_Description = @Program_Description
									
									WHERE	Program_Id = @Program_Id;

									/* Delete existing program-exercise relationships */

									DELETE	FROM rltn_program_exercise
									WHERE	Program_Id = @Program_Id;";

			string exerciseSql = @"	/* Insert program-exercise relationships */

									INSERT	INTO rltn_program_exercise
									(	Program_Id,
										Exercise_Id,
										Position,
										Sets,
										Reps,
										Weight,
										Duration
									)
									VALUES(
										@Program_Id,
										@Exercise_Id,
										@Exercise_Position,
										@Exercise_Sets,
										@Exercise_Reps,
										@Exercise_Weight,
										@Exercise_Duration
									);";

			var programEngine = _programMySqlProvider.GetEngine();
			var exerciseEngine = _exerciseMySqlProvider.GetEngine();

			List<MySqlParameter> programParameters = (List<MySqlParameter>)ProgramParameters(program);
			programEngine.UpdateItem(programSql, programParameters);

			foreach(Exercise exercise in program.Exercises)
			{
				List<MySqlParameter> exerciseParameters = (List<MySqlParameter>)ExerciseParameters(exercise);
				exerciseParameters.Add(new MySqlParameter("@Program_Id", program.Id));

				exerciseEngine.UpdateItem(exerciseSql, exerciseParameters);
			}
		}

		private IList<MySqlParameter> ProgramParameters(Program program)
		{
			List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
			{
				new MySqlParameter("@Program_Id", program.Id),
				new MySqlParameter("@User_Id", program.UserId),
				new MySqlParameter("@Program_Name", program.Name),
				new MySqlParameter("@Program_Description", program.Description),
				new MySqlParameter("@Program_DateCreated", program.DateCreated)
			};

			return mySqlParameters;
		}

		private IList<MySqlParameter> ExerciseParameters(Exercise exercise)
		{
			List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
			{
				new MySqlParameter("@Exercise_Id", exercise.Id),
				new MySqlParameter("@Exercise_Name", exercise.Name),
				new MySqlParameter("@Exercise_Position", exercise.Position),
				new MySqlParameter("@Exercise_Sets", exercise.Sets),
				new MySqlParameter("@Exercise_Reps", exercise.Reps),
				new MySqlParameter("@Exercise_Weight", exercise.Weight),
				new MySqlParameter("@Exercise_Duration", exercise.Duration)
			};

			return mySqlParameters;
		}
	}
}