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

		public IResult Handle(IRequestDetails requestDetails, IEndpointDetails endpoint)
		{
			return requestDetails.IsAuthorized
				       ? _successor.Handle(requestDetails, endpoint)
				       : UnauthorizedResult.ForInsufficientPrivileges();
		}
	}
}