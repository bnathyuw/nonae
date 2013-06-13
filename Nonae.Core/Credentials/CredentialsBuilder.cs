namespace Nonae.Core.Credentials
{
	static internal class CredentialsBuilder
	{
		public static ICredentials From(string authorizationHeader)
		{
			if (authorizationHeader == null) return new AnonymousCredentials();

			var authorizationHeaderBits = authorizationHeader.Split(' ');

			var authorizationType = authorizationHeaderBits[0];

			switch (authorizationType)
			{
				case "Basic":
					return new BasicCredentials(authorizationHeaderBits[1]);
				default:
					return new InvalidCredentials();
			}
			
		}
	}
}