using Nonae.Core.Endpoints;
using Nonae.Core.Requests;
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

		public IResult Handle(IRequestDetails requestDetails, IEndpointDetails endpoint)
		{
            return endpoint.ResourceExists
				? _resourceFoundSuccessor.Handle(requestDetails, endpoint) 
				: _resourceNotFoundSuccessor.Handle(requestDetails, endpoint);
		}
	}
}