using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gainshark_api.Encryption.Contract
{
	public interface IBCryptEncryption
	{
		string Hash(string password, int workFactor = 0);
		void SetWorkFactor(int workFactor);
		bool Verify(string password, string hash);
	}
}
