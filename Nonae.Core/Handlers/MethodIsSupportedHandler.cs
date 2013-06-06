using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	internal class MethodIsSupportedHandler : IHandler
	{
		private readonly IHandler _successor;

		public MethodIsSupportedHandler()
		{
			_successor = new OkHandler();
		}

		public IResult Handle(RequestDetails requestDetails)
		{
			return requestDetails.Endpoint.Allows(requestDetails)
				       ? _successor.Handle(requestDetails)
				       : new MethodNotAllowedResult(requestDetails.Endpoint);
		}
	}
}