using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nonae.Core.Authorization
{
	internal class CredentialsBuilder
	{
		private const string UnsupportedAuthorizationMethod = "Unsupported Authorization Method";
		private const string UserNotFound = "User Not Found";
		private readonly IAuthenticationProvider _authenticationProvider;

		public CredentialsBuilder(IAuthenticationProvider authenticationProvider)
		{
			_authenticationProvider = authenticationProvider;
		}

		public Credentials From(string authorizationHeader)
		{
			if (authorizationHeader == null) return AnonymousUserCredentials();

			var authorizationHeaderBits = authorizationHeader.Split(' ');

			var authorizationType = authorizationHeaderBits[0];

			switch (authorizationType)
			{
				case "Basic":
					return BuildBasicCredentials(authorizationHeaderBits);
				default:
					return UnsupportedAuthorizationMethodCredentials();
			}
		}

		private Credentials BuildBasicCredentials(IList<string> authorizationHeaderBits)
		{
			var credentialBytes = Convert.FromBase64String(authorizationHeaderBits[1]);
			var getString = Encoding.Unicode.GetString(credentialBytes);
			var strings = getString.Split(':');
			var username = strings.First();
			var password = strings.ElementAt(1);
			var authenticate = _authenticationProvider.Authenticate(username, password);
			return authenticate ? AuthenticatedUserCredentials(username) : UserNotFoundCredentials();
		}

		private static Credentials AuthenticatedUserCredentials(string username)
		{
			return new Credentials(false, username, null);
		}

		private static Credentials AnonymousUserCredentials()
		{
			return new Credentials(true, null, null);
		}

		private static Credentials UnsupportedAuthorizationMethodCredentials()
		{
			return new Credentials(false, null, UnsupportedAuthorizationMethod);
		}

		private static Credentials UserNotFoundCredentials()
		{
			return new Credentials(false, null, UserNotFound);
		}
	}
}