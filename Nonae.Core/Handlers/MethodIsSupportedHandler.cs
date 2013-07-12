using Nonae.Core.Endpoints;
using Nonae.Core.Requests;
using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	internal class MethodIsSupportedHandler : IHandler
	{
		private readonly IHandler _successor;

		public MethodIsSupportedHandler(IHandler successor)
		{
			_successor = successor;
		}

		public IResult Handle(IRequestDetails requestDetails, IEndpointDetails endpoint)
		{
			return requestDetails.MethodIsSupported
				       ? _successor.Handle(requestDetails, endpoint)
				       : new MethodNotAllowedResult(endpoint);
		}
	}
}