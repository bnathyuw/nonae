using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nonae.Core.Credentials
{
	internal class CredentialsBuilder
	{
		private readonly IAuthenticationProvider _authenticationProvider;

		public CredentialsBuilder(IAuthenticationProvider authenticationProvider)
		{
			_authenticationProvider = authenticationProvider;
		}

		public ICredentials From(string authorizationHeader)
		{
			if (authorizationHeader == null) return new AnonymousCredentials();

			var authorizationHeaderBits = authorizationHeader.Split(' ');

			var authorizationType = authorizationHeaderBits[0];

			switch (authorizationType)
			{
				case "Basic":
					return BuildBasicCredentials(authorizationHeaderBits);
				default:
					return new InvalidCredentials("Unsupported Authorization Method");
			}
		}

		private ICredentials BuildBasicCredentials(IList<string> authorizationHeaderBits)
		{
			var credentialBytes = Convert.FromBase64String(authorizationHeaderBits[1]);
			var getString = Encoding.Unicode.GetString(credentialBytes);
			var strings = getString.Split(':');
			var username = strings.First();
			var password = strings.ElementAt(1);
			var authenticate = _authenticationProvider.Authenticate(username, password);
			return authenticate ? (ICredentials) new BasicCredentials(username) : new InvalidCredentials("Invalid Credentials");
		}
	}
}