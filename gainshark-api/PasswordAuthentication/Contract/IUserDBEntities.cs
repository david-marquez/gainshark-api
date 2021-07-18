using gainshark_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gainshark_api.PasswordAuthentication.Contract
{
	public interface IUserDBEntities
	{
		IList<User> Users();
	}
}
