using gainshark_api.Authentication.Attribute;
using gainshark_api.Authentication.Implementation;
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
	//[BasicAuthentication]
	[RoutePrefix("api/programs")]
    public class ProgramsController : ApiController
    {
		private IDataAccess<Program> _dataAccess;

		public ProgramsController(IDataAccess<Program> dataAccess)
		{
			_dataAccess = dataAccess;
		}

		[HttpPost]
		[Route("add")]
		public HttpResponseMessage AddProgram([FromBody]Program program)
		{
			try
			{
				_dataAccess.AddItem(program);
				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, $"Added program {program.Name}");
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
		public HttpResponseMessage DeleteProgram(int id)
		{
			try
			{
				_dataAccess.DeleteItem(id);
				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, $"Deleted program");
				return response;
			}
			catch(Exception e)
			{
				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, $"Error {e.Message}");
				return response;
			}
		}

		[HttpGet]
		[Route("{id}")]
		public Program GetProgram(int id)
		{
			return _dataAccess.GetItem(id);
		}
		
		[HttpGet]
		[Route("")]
		public IList<Program> GetPrograms()
		{
			return _dataAccess.GetItems();
		}

		[HttpPost]
		[Route("update")]
		public HttpResponseMessage UpdateProgram([FromBody]Program program)
		{
			try
			{
				_dataAccess.UpdateItem(program);
				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, $"Updated program {program.Name}");
				return response;
			}
			catch(Exception e)
			{
				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, $"Error {e.Message}");
				return response;
			}
		}
    }
}
