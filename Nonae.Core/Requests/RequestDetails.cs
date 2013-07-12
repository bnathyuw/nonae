using System.Net.Http;
using Nonae.Core.Endpoints;

namespace Nonae.Core.Requests
{
	public class RequestDetails : IRequestDetails
	{
		internal RequestDetails(HttpMethod httpMethod)
		{
		    _httpMethod = httpMethod;
		}

	    private readonly HttpMethod _httpMethod;

	    public bool GetMethodIsSupported(IEndpointDetails endpointDetails)
	    {
	        return endpointDetails.Allows(_httpMethod);
	    }

	    public bool Answers(HttpMethod httpMethod)
		{
			return _httpMethod == httpMethod;
		}
	}
}