using Nonae.Core.Authorization;
using Nonae.Core.Endpoints;
using Nonae.Core.Requests;
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

		public IResult Handle(IRequestDetails requestDetails, IEndpointDetails endpoint, ICredentials credentials)
		{
		    return !credentials.IsAnonymous
		               ? CheckCredentials(requestDetails, endpoint, credentials)
		               : _successor.Handle(requestDetails, endpoint, credentials);
		}

		private IResult CheckCredentials(IRequestDetails requestDetails, IEndpointDetails endpoint, ICredentials credentials)
		{
			return credentials.IsAuthenticated
				       ? _successor.Handle(requestDetails, endpoint, credentials)
				       : new UnauthorizedResult(credentials.FailureMessage);
		}
	}
}
