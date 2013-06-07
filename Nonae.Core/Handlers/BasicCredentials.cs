using System;
using System.Text;

namespace Nonae.Core.Handlers
{
	public class BasicCredentials:Credentials
	{
		private readonly string _encodedCredentials;

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
				return credentials == "username:password";
			}
		}

		public override bool AuthorizationMethodIsSupported
		{
			get { return true; }
		}
	}
}