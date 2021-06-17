using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gainshark_api.Models
{
	public class Exercise
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public byte[] Image { get; set; }
		public int Position { get; set; }
		public int Reps { get; set; }
		public int Sets { get; set; }
		public int Duration { get; set; }
		public int Weight { get; set; }
		public IList<MuscleGroup> MuscleGroups { get; set; }
	}
}