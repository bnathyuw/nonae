using System;
using System.Text;

namespace Nonae.Core.Handlers
{
	public class BasicAuthorizationDetails:AuthorizationDetails
	{
		private readonly string _encodedCredentials;

		internal BasicAuthorizationDetails(string encodedCredentials)
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
	}
}