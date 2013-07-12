using System.Net.Http;
using Nonae.Core.Authorization;
using Nonae.Core.Endpoints;
using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	internal class AuthenticationHandler : IHandler
	{
		private readonly IHandler _successor;

		public AuthenticationHandler(IHandler successor)
		{
			_successor = successor;
		}

		public IResult Handle(IEndpointDetails endpoint, ICredentials credentials, HttpMethod httpMethod)
		{
		    return !credentials.IsAnonymous
		               ? CheckCredentials(endpoint, credentials, httpMethod)
		               : _successor.Handle(endpoint, credentials, httpMethod);
		}

		private IResult CheckCredentials(IEndpointDetails endpoint, ICredentials credentials, HttpMethod httpMethod)
		{
			return credentials.IsAuthenticated
                       ? _successor.Handle(endpoint, credentials, httpMethod)
				       : new UnauthorizedResult(credentials.FailureMessage);
		}
	}
}
