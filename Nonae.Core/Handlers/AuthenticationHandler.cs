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

		public IResult Handle(IRequestDetails requestDetails, IEndpointDetails endpoint)
		{
			return requestDetails.HasAuthorization
				       ? CheckCredentials(requestDetails,endpoint)
				       : _successor.Handle(requestDetails, endpoint);
		}

		private IResult CheckCredentials(IRequestDetails requestDetails, IEndpointDetails endpoint)
		{
			return requestDetails.IsAuthenticated
				       ? _successor.Handle(requestDetails, endpoint)
				       : new UnauthorizedResult(requestDetails.AuthenticationFailureMessage);
		}
	}
}
