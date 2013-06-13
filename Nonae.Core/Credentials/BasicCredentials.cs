using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nonae.Core.Credentials
{
	public class BasicCredentials:Credentials
	{
		private readonly IEnumerable<string> _users = new List<string> {"username:password", "admin:password"};
		private readonly string _credentials;

		internal BasicCredentials(string encodedCredentials)
		{
			var credentialBytes = Convert.FromBase64String(encodedCredentials);
			_credentials = Encoding.Unicode.GetString(credentialBytes);
		}

		public override bool IsAuthenticated
		{
			get
			{
				return _users.Contains(_credentials);
			}
		}

		public override bool AuthorizationMethodIsSupported
		{
			get { return true; }
		}

		public override string Username
		{
			get
			{
				var bits = _credentials.Split(':');
				return bits.First();
			}
		}
	}
}