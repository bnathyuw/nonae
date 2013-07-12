using System.Net.Http;
using Nonae.Core.Endpoints;

namespace Nonae.Core.Requests
{
	public interface IRequestDetails
	{
	    bool IsAuthenticated { get; }
	    bool GetIsAuthorized(IEndpointDetails endpointDetails);
	    bool GetMethodIsSupported(IEndpointDetails endpointDetails);
	    string AuthenticationFailureMessage { get; }
	    bool Answers(HttpMethod httpMethod);
	}
}