using System.Net.Http;
using Nonae.Core.Authorization;
using Nonae.Core.Endpoints;
using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	public interface IHandler
	{
		IResult Handle(IEndpointDetails endpoint, ICredentials credentials, HttpMethod httpMethod);
	}
}