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
	[RoutePrefix("users")]
    public class UsersController : ApiController
    {
		private IDataAccess<User> _dataAccess;

		public UsersController(IDataAccess<User> dataAccess)
		{
			_dataAccess = dataAccess;
		}

		[HttpPost]
		[Route("add")]
		public HttpResponseMessage AddUser([FromBody]User user)
		{
			try
			{
				_dataAccess.AddItem(user);
				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, $"Added user {user.UserName}");
				return response;
			}
			catch(Exception e)
			{
				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, $"Error {e.Message}");
				return response;
			}
		}

		[HttpPost]
		[Route("delete/{id}")]
		public HttpResponseMessage DeleteUser(int id)
		{
			try
			{
				_dataAccess.DeleteItem(id);
				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, $"Deleted user");
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
		public User GetUser(int id)
		{
			return _dataAccess.GetItem(id);
		}

		[HttpGet]
		[Route("")]
		public IList<User> GetUsers()
		{
			return _dataAccess.GetItems();
		}

		[HttpPost]
		[Route("update")]
		public HttpResponseMessage UpdateUser([FromBody]User user)
		{
			try
			{
				_dataAccess.UpdateItem(user);
				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, $"Updated user {user.UserName}");
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
