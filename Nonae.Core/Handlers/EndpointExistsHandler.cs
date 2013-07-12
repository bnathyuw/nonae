using Nonae.Core.Authorization;
using Nonae.Core.Endpoints;
using Nonae.Core.Requests;
using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	internal class EndpointExistsHandler : IHandler
	{
		private readonly IHandler _successor;

		public EndpointExistsHandler(IHandler successor)
		{
			_successor = successor;
		}

		public IResult Handle(IRequestDetails requestDetails, IEndpointDetails endpoint, ICredentials credentials)
		{
			return endpoint.Exists
				       ? _successor.Handle(requestDetails, endpoint, credentials)
				       : NotFoundResult.ForUnknownAddress();
		}
	}
}