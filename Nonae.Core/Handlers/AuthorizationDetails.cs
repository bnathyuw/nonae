namespace Nonae.Core.Handlers
{
	public abstract class AuthorizationDetails
	{
		public abstract bool IsAuthenticated { get; }

		public static AuthorizationDetails From(string authorizationHeader)
		{
			var authorizationHeaderBits = authorizationHeader.Split(' ');

			var authorizationType = authorizationHeaderBits[0];

			return authorizationType != "Basic" ? null : new BasicAuthorizationDetails(authorizationHeaderBits[1]);
		}
	}
}