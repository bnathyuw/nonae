using Nonae.Core.Authorization;
using Nonae.Core.Endpoints;
using Nonae.Core.Requests;
using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	internal class OkHandler : IHandler
	{
		public IResult Handle(IRequestDetails requestDetails, IEndpointDetails endpoint, ICredentials credentials)
		{
			return new OkResult(endpoint);
		}
	}
}