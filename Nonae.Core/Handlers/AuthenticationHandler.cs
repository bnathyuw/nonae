using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	internal class AuthenticationHandler : IHandler
	{
		private readonly IHandler _successor;

		public AuthenticationHandler()
		{
			_successor = new OptionsHandler();
		}

		public IResult Handle(RequestDetails requestDetails)
		{
			var authorizationHeader = requestDetails.Headers["Authorization"];

			return authorizationHeader == null 
				? _successor.Handle(requestDetails) 
				: CheckAuthenticationFromHeader(authorizationHeader, requestDetails);
		}

		private IResult CheckAuthenticationFromHeader(string authorizationHeader, RequestDetails requestDetails)
		{
			var authorizationDetails = AuthorizationDetails.From(authorizationHeader);

			return authorizationDetails == null
				       ? UnauthorizedResult.ForUnsupportedAuthorizationMethod()
				       : CheckBasicAuth(authorizationDetails, requestDetails);
		}

		private IResult CheckBasicAuth(AuthorizationDetails authorizationDetails, RequestDetails requestDetails)
		{
			return authorizationDetails.IsAuthenticated
				       ? _successor.Handle(requestDetails)
				       : UnauthorizedResult.ForInvalidCredentials();
		}
	}
}
