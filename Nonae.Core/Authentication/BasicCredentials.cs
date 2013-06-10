using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nonae.Core.Authentication
{
	public class BasicCredentials:Credentials
	{
		private readonly string _encodedCredentials;
		private readonly IEnumerable<string> _users = new List<string> {"username:password", "admin:password"};

		internal BasicCredentials(string encodedCredentials)
		{
			_encodedCredentials = encodedCredentials;
		}

		public override bool IsAuthenticated
		{
			get
			{
				var credentialBytes = Convert.FromBase64String(_encodedCredentials);
				var credentials = Encoding.Unicode.GetString(credentialBytes);
				return _users.Contains(credentials);
			}
		}

		public override bool AuthorizationMethodIsSupported
		{
			get { return true; }
		}
	}
}