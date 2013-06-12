using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	internal class OptionsHandler : IHandler
	{
		private readonly IHandler _successor;

		public OptionsHandler(IHandler endpointExistsHandler)
		{
			_successor = endpointExistsHandler;
		}

		public IResult Handle(RequestDetails requestDetails)
		{
			// TODO: Authorize against endpoint?

			return requestDetails.IsOptionsRequest
				       ? new OptionsResult(requestDetails)
				       : _successor.Handle(requestDetails);
		}
	}
}