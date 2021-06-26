using gainshark_api.Mappers.Contract;
using gainshark_api.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gainshark_api.Mappers.Implementation
{
	public class MuscleGroupMapper : IMapper<MuscleGroup>
	{
		public MuscleGroup Map(MySqlDataReader dataReader)
		{
			MuscleGroup muscleGroup = new MuscleGroup();

			muscleGroup.Id = Convert.ToInt32(dataReader[0]);
			muscleGroup.Name = dataReader[1] as string ?? null;
			muscleGroup.Description = dataReader[2] as string ?? null;
			muscleGroup.Image = dataReader.IsDBNull(3) ? null : (byte[])dataReader[3];

			return muscleGroup;
		}
	}
}