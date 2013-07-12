using System.Net.Http;
using Nonae.Core.Authorization;
using Nonae.Core.Endpoints;
using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	internal class EndpointExistsHandler : IHandler
	{
		private readonly IHandler _successor;

		public EndpointExistsHandler(IHandler successor)
		{
			_successor = successor;
		}

		public IResult Handle(IEndpointDetails endpoint, ICredentials credentials, HttpMethod httpMethod)
		{
			return endpoint.Exists
				       ? _successor.Handle(endpoint, credentials, httpMethod)
				       : NotFoundResult.ForUnknownAddress();
		}
	}
}