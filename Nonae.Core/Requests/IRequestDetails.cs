using System.Net.Http;
using Nonae.Core.Endpoints;

namespace Nonae.Core.Requests
{
	public interface IRequestDetails
	{
	    bool GetMethodIsSupported(IEndpointDetails endpointDetails);
	    bool Answers(HttpMethod httpMethod);
	}
}