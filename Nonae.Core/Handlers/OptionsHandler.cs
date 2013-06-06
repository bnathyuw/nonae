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

			return IsOptions(requestDetails)
				       ? new OptionsResult(EndpointStore.Get(requestDetails))
				       : _successor.Handle(requestDetails);
		}

		private static bool IsOptions(RequestDetails requestDetails)
		{
			return requestDetails.Matches(HttpMethod.Options);
		}
	}
}