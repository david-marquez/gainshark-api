using gainshark_api.DataAccess.Contract;
using gainshark_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace gainshark_api.Controllers
{
	[RoutePrefix("api/exercises")]
    public class ExercisesController : ApiController
    {
		private IDataAccess<Exercise> _dataAccess;

		public ExercisesController(IDataAccess<Exercise> dataAccess)
		{
			_dataAccess = dataAccess;
		}

		[HttpPost]
		[Route("add")]
		public HttpResponseMessage AddExercise([FromBody]Exercise exercise)
		{
			try
			{
				_dataAccess.AddItem(exercise);
				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, $"Added exercise {exercise.Name}");
				return response;
			}
			catch(Exception e)
			{
				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, $"Error. {e.Message}");
				return response;
			}
		}

		[HttpPost]
		[Route("delete/{id}")]
		public HttpResponseMessage DeleteExercise(int id)
		{
			try
			{
				_dataAccess.DeleteItem(id);
				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, $"Deleted exercise");
				return response;
			}
			catch(Exception e)
			{
				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, $"Error. {e.Message}");
				return response;
			}
		}

		[HttpGet]
		[Route("{id}")]
		public Exercise GetExercise(int id)
		{
			return _dataAccess.GetItem(id);
		}

		[HttpGet]
		[Route("")]
		public IList<Exercise> GetExercises()
		{
			return _dataAccess.GetItems();
		}

		[HttpPost]
		[Route("update")]
		public HttpResponseMessage UpdateExercise([FromBody]Exercise exercise)
		{
			try
			{
				_dataAccess.UpdateItem(exercise);
				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, $"Updated exercise {exercise.Name}");
				return response;
			}
			catch(Exception e)
			{
				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, $"Error. {e.Message}");
				return response;
			}
		}
    }
}
