using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	internal class OptionsHandler : IHandler
	{
		private readonly IHandler _successor;

		public OptionsHandler(IHandler successor)
		{
			_successor = successor;
		}

		public IResult Handle(IRequestDetails requestDetails)
		{
			// TODO: Authorize against endpoint?

			return requestDetails.IsOptionsRequest
				       ? new OptionsResult(requestDetails)
				       : _successor.Handle(requestDetails);
		}
	}
}