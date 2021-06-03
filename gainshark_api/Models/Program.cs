using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gainshark_api.Models
{
	public class Program
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public IList<Exercise> Exercises { get; set; }
	}
}