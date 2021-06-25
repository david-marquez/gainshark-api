using gainshark_api.Encryption.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gainshark_api.Encryption.Implementation
{
	public class BCryptEncryption : IBCryptEncryption
	{
		private int _workFactor = 10;

		public string Hash(string password, int workFactor)
		{
			if(workFactor == 0)
			{
				return BCrypt.Net.BCrypt.HashPassword(password, _workFactor);
			}
			else
			{
				return BCrypt.Net.BCrypt.HashPassword(password, workFactor);
			}
		}

		public void SetWorkFactor(int workFactor)
		{
			_workFactor = workFactor;
		}

		public bool Verify(string password, string hash)
		{
			return BCrypt.Net.BCrypt.Verify(password, hash);
		}
	}
}