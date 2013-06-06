namespace Nonae.Core.Handlers
{
	public abstract class Credentials
	{
		public abstract bool IsAuthenticated { get; }

		public static Credentials From(string authorizationHeader)
		{
			var authorizationHeaderBits = authorizationHeader.Split(' ');

			var authorizationType = authorizationHeaderBits[0];

			return authorizationType != "Basic" ? null : new BasicCredentials(authorizationHeaderBits[1]);
		}
	}
}