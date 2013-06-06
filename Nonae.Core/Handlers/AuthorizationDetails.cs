using System;
using System.Text;

namespace Nonae.Core.Handlers
{
	public class AuthorizationDetails
	{
		private readonly string _encodedCredentials;

		private AuthorizationDetails(string encodedCredentials)
		{
			_encodedCredentials = encodedCredentials;
		}

		public bool IsAuthenticated
		{
			get
			{
				var credentialBytes = Convert.FromBase64String(_encodedCredentials);
				var credentials = Encoding.Unicode.GetString(credentialBytes);
				return credentials == "username:password";
			}
		}

		public static AuthorizationDetails From(string authorizationHeader)
		{
			var authorizationHeaderBits = authorizationHeader.Split(' ');

			var authorizationType = authorizationHeaderBits[0];

			return authorizationType != "Basic" ? null : new AuthorizationDetails(authorizationHeaderBits[1]);
		}
	}
}