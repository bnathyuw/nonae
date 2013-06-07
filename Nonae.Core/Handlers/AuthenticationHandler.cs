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
			return requestDetails.HasAuthorization 
				? _successor.Handle(requestDetails) 
				: CheckAuthenticationFromHeader(requestDetails);
		}

		private IResult CheckAuthenticationFromHeader(RequestDetails requestDetails)
		{
			return requestDetails.AuthorizationMethodIsSupported
				       ? CheckCredentials(requestDetails)
				       : UnauthorizedResult.ForUnsupportedAuthorizationMethod();
		}

		private IResult CheckCredentials(RequestDetails requestDetails)
		{
			return requestDetails.IsAuthenticated
				       ? _successor.Handle(requestDetails)
				       : UnauthorizedResult.ForInvalidCredentials();
		}
	}
}
