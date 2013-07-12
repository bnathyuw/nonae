using Nonae.Core.Authorization;
using Nonae.Core.Endpoints;
using Nonae.Core.Requests;
using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	internal class AuthorizationHandler : IHandler
	{
		private readonly IHandler _successor;

		public AuthorizationHandler(IHandler successor)
		{
			_successor = successor;
		}

		public IResult Handle(IRequestDetails requestDetails, IEndpointDetails endpoint, ICredentials credentials)
		{
			return requestDetails.GetIsAuthorized(endpoint)
				       ? _successor.Handle(requestDetails, endpoint, credentials)
				       : UnauthorizedResult.ForInsufficientPrivileges();
		}
	}
}