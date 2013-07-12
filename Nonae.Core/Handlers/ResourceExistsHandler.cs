using System.Net.Http;
using Nonae.Core.Authorization;
using Nonae.Core.Endpoints;
using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	public class ResourceExistsHandler:IHandler
	{
		private readonly IHandler _resourceFoundSuccessor;
		private readonly IHandler _resourceNotFoundSuccessor;

		public ResourceExistsHandler(IHandler resourceFoundSuccessor, IHandler resourceNotFoundSuccessor)
		{
			_resourceFoundSuccessor = resourceFoundSuccessor;
			_resourceNotFoundSuccessor = resourceNotFoundSuccessor;
		}

		public IResult Handle(IEndpointDetails endpoint, ICredentials credentials, HttpMethod httpMethod)
		{
            return endpoint.ResourceExists
				? _resourceFoundSuccessor.Handle(endpoint, credentials, httpMethod) 
				: _resourceNotFoundSuccessor.Handle(endpoint, credentials, httpMethod);
		}
	}
}