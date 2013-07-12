using System.Net.Http;
using Nonae.Core.Authorization;
using Nonae.Core.Endpoints;
using Nonae.Core.Requests;
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

		public IResult Handle(IEndpointDetails endpoint, ICredentials credentials, HttpMethod httpMethod)
		{
			// _endpointDetails: Authorize against endpoint?

			return httpMethod == HttpMethod.Options
				       ? new OptionsResult(endpoint)
				       : _successor.Handle(endpoint, credentials, httpMethod);
		}
	}
}