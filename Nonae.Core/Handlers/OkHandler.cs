using System.Net.Http;
using Nonae.Core.Authorization;
using Nonae.Core.Endpoints;
using Nonae.Core.Requests;
using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	internal class OkHandler : IHandler
	{
		public IResult Handle(IEndpointDetails endpoint, ICredentials credentials, HttpMethod httpMethod)
		{
			return new OkResult(endpoint);
		}
	}
}