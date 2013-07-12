using System.Net.Http;
using Nonae.Core.Authorization;
using Nonae.Core.Endpoints;
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

		public IResult Handle(IEndpointDetails endpoint, ICredentials credentials, HttpMethod httpMethod)
		{
			return endpoint.Allows(httpMethod)
				       ? _successor.Handle(endpoint, credentials, httpMethod)
				       : new MethodNotAllowedResult(endpoint);
		}
	}
}