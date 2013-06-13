using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nonae.Core.Credentials
{
	public class BasicCredentials:ICredentials
	{
		private readonly IEnumerable<string> _users = new List<string> {"username:password", "admin:password"};
		private readonly string _credentials;

		internal BasicCredentials(string encodedCredentials)
		{
			var credentialBytes = Convert.FromBase64String(encodedCredentials);
			_credentials = Encoding.Unicode.GetString(credentialBytes);
		}

		public bool IsAuthenticated
		{
			get
			{
				return _users.Contains(_credentials);
			}
		}

		public bool AuthorizationMethodIsSupported
		{
			get { return true; }
		}

		public string Username
		{
			get
			{
				var bits = _credentials.Split(':');
				return bits.First();
			}
		}

		public bool IsAnonymous
		{
			get { return false; }
		}
	}
}