namespace Nonae.Core.Authentication
{
	public abstract class Credentials
	{
		public abstract bool IsAuthenticated { get; }

		public abstract bool AuthorizationMethodIsSupported { get; }

		public abstract string Username { get; }

		public static Credentials From(string authorizationHeader)
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