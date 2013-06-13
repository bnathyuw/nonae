using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nonae.Core.Authorization
{
	internal class CredentialsBuilder
	{
		private readonly IAuthenticationProvider _authenticationProvider;

		public CredentialsBuilder(IAuthenticationProvider authenticationProvider)
		{
			_authenticationProvider = authenticationProvider;
		}

		public Credentials From(string authorizationHeader)
		{
			if (authorizationHeader == null) return Credentials.ForAnonymousUser();

			var authorizationHeaderBits = authorizationHeader.Split(' ');

			var authorizationType = authorizationHeaderBits[0];

			switch (authorizationType)
			{
				case "Basic":
					return BuildBasicCredentials(authorizationHeaderBits);
				default:
					return Credentials.ForUnsupportedAuthorizationMethod();
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
			return authenticate ? Credentials.ForAuthenticatedUser(username) : Credentials.ForUserNotFound();
		}
	}
}