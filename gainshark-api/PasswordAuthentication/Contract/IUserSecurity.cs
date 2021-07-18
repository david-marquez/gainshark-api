using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gainshark_api.PasswordAuthentication.Contract
{
	public interface IUserSecurity
	{
		bool Login(string userName, string password);
	}
}
