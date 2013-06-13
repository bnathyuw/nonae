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

		public IResult Handle(IRequestDetails requestDetails)
		{
			return requestDetails.MethodIsSupported
				       ? _successor.Handle(requestDetails)
				       : new MethodNotAllowedResult(requestDetails);
		}
	}
}