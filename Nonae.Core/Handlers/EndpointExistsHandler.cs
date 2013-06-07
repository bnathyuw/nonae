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
			return requestDetails.EndpointExists
				       ? _successor.Handle(requestDetails)
				       : new NotFoundResult();
		}
	}
}