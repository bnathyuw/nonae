using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	internal class AuthenticationHandler : IHandler
	{
		private readonly IHandler _successor;

		public AuthenticationHandler(IHandler successor)
		{
			_successor = successor;
		}

		public IResult Handle(IRequestDetails requestDetails)
		{
			return requestDetails.HasAuthorization 
				? CheckAuthenticationFromHeader(requestDetails) 
				: _successor.Handle(requestDetails);
		}

		private IResult CheckAuthenticationFromHeader(IRequestDetails requestDetails)
		{
			return requestDetails.AuthorizationMethodIsSupported
				       ? CheckCredentials(requestDetails)
				       : UnauthorizedResult.ForUnsupportedAuthorizationMethod();
		}

		private IResult CheckCredentials(IRequestDetails requestDetails)
		{
			return requestDetails.IsAuthenticated
				       ? _successor.Handle(requestDetails)
				       : UnauthorizedResult.ForInvalidCredentials();
		}
	}
}
