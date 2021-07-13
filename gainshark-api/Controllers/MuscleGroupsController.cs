using gainshark_api.Authentication.Attribute;
using gainshark_api.DataAccess.Contract;
using gainshark_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace gainshark_api.Controllers
{
	[EnableCors(origins: "*", headers: "*", methods: "*")]
	[BasicAuthentication]
	[RoutePrefix("api/musclegroups")]
    public class MuscleGroupsController : ApiController
    {
		private IDataAccess<MuscleGroup> _dataAccess;

		public MuscleGroupsController(IDataAccess<MuscleGroup> dataAccess)
		{
			_dataAccess = dataAccess;
		}

		[HttpPost]
		[Route("add")]
		public HttpResponseMessage AddMuscleGroup([FromBody]MuscleGroup muscleGroup)
		{
			try
			{
				_dataAccess.AddItem(muscleGroup);
				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, $"Added muscle group {muscleGroup.Name}");
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
		public HttpResponseMessage DeleteMuscleGroup(int id)
		{
			try
			{
				_dataAccess.DeleteItem(id);
				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, $"Deleted muscle group");
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
		public MuscleGroup GetMuscleGroup(int id)
		{
			return _dataAccess.GetItem(id);
		}

		[HttpGet]
		[Route("")]
		public IList<MuscleGroup> GetMuscleGroups()
		{
			return _dataAccess.GetItems();
		}

		[HttpPost]
		[Route("update")]
		public HttpResponseMessage UpdateMuscleGroup([FromBody]MuscleGroup muscleGroup)
		{
			try
			{
				_dataAccess.UpdateItem(muscleGroup);
				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, $"Updated muscle group {muscleGroup.Name}");
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
