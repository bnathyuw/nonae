using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	internal class MethodIsSupportedHandler : IHandler
	{
		private readonly IHandler _successor;

		public MethodIsSupportedHandler(IHandler okHandler)
		{
			_successor = okHandler;
		}

		public IResult Handle(RequestDetails requestDetails)
		{
			return requestDetails.MethodIsSupported
				       ? _successor.Handle(requestDetails)
				       : new MethodNotAllowedResult(requestDetails);
		}
	}
}