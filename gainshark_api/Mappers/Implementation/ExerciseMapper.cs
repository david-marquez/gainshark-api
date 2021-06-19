using gainshark_api.Mappers.Contract;
using gainshark_api.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gainshark_api.Mappers.Implementation
{
	public class ExerciseMapper : IMapper<Exercise>
	{
		public Exercise Map(MySqlDataReader dataReader)
		{
			Exercise exercise = new Exercise();

			exercise.Id = Convert.ToInt32(dataReader[0]);
			exercise.Name = dataReader[1] as string ?? null;
			exercise.Description = dataReader[2] as string ?? null;
			exercise.Image = dataReader.IsDBNull(3) ? null : (byte[])dataReader[3];
			exercise.Position = dataReader.IsDBNull(4) ? 0 : Convert.ToInt32(dataReader[4]);
			exercise.Reps = dataReader.IsDBNull(5) ? 0 : Convert.ToInt32(dataReader[5]);
			exercise.Sets = dataReader.IsDBNull(6) ? 0 : Convert.ToInt32(dataReader[6]);
			exercise.Duration = dataReader.IsDBNull(7) ? 0 : Convert.ToInt32(dataReader[7]);
			exercise.Weight = dataReader.IsDBNull(8) ? 0 : Convert.ToInt32(dataReader[8]);

			return exercise;
		}
	}
}