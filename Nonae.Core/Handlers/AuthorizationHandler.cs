using System.Net.Http;
using Nonae.Core.Authorization;
using Nonae.Core.Endpoints;
using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	internal class AuthorizationHandler : IHandler
	{
		private readonly IHandler _successor;

		public AuthorizationHandler(IHandler successor)
		{
			_successor = successor;
		}

		public IResult Handle(IEndpointDetails endpoint, ICredentials credentials, HttpMethod httpMethod)
		{
			return endpoint.IsAuthorizedFor(credentials)
				       ? _successor.Handle(endpoint, credentials, httpMethod)
				       : UnauthorizedResult.ForInsufficientPrivileges();
		}
	}
}