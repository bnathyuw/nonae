using Nonae.Core.Authorization;
using Nonae.Core.Endpoints;
using Nonae.Core.Requests;
using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	public interface IHandler
	{
		IResult Handle(IRequestDetails requestDetails, IEndpointDetails endpoint, ICredentials credentials);
	}
}