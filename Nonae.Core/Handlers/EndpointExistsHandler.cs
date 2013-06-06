using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	internal class EndpointExistsHandler : IHandler
	{
		private readonly IHandler _successor;

		public EndpointExistsHandler()
		{
			_successor = new MethodIsSupportedHandler();
		}

		public IResult Handle(RequestDetails requestDetails)
		{
			var endpoint = EndpointStore.Get(requestDetails);
			requestDetails.Endpoint = endpoint;
			return endpoint == null
				       ? new NotFoundResult()
				       : _successor.Handle(requestDetails);
		}
	}
}