using System.Net.Http;
using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	internal class OptionsHandler : IHandler
	{
		private readonly IHandler _successor;

		public OptionsHandler()
		{
			_successor = new EndpointExistsHandler();
		}

		public IResult Handle(RequestDetails requestDetails)
		{
			// TODO: Authorize against endpoint?

			return requestDetails.IsOptionsRequest
				       ? new OptionsResult(EndpointStore.Get(requestDetails))
				       : _successor.Handle(requestDetails);
		}
	}
}