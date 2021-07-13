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
	[RoutePrefix("api/roles")]
    public class RolesController : ApiController
    {
		private IDataAccess<Role> _dataAccess;

		public RolesController(IDataAccess<Role> dataAccess)
		{
			_dataAccess = dataAccess;
		}

		[HttpPost]
		[Route("add")]
		public HttpResponseMessage AddRole([FromBody]Role role)
		{
			try
			{
				_dataAccess.AddItem(role);
				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, $"Added role {role.Name}");
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
		public HttpResponseMessage DeleteRole(int id)
		{
			try
			{
				_dataAccess.DeleteItem(id);
				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, $"Deleted role");
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
		public Role GetRole(int id)
		{
			return _dataAccess.GetItem(id);
		}

		[HttpGet]
		[Route("")]
		public IList<Role> GetRoles()
		{
			return _dataAccess.GetItems();
		}

		[HttpPost]
		[Route("update")]
		public HttpResponseMessage UpdateRole([FromBody]Role role)
		{
			try
			{
				_dataAccess.UpdateItem(role);
				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, $"Updated role {role.Name}");
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
