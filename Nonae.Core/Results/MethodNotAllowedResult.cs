using System.Net;
using Nonae.Core.Endpoints;
using Nonae.Core.Responses;

namespace Nonae.Core.Results
{
	public class MethodNotAllowedResult : IResult
	{
	    private readonly IEndpointDetails _endpointDetails;

	    public MethodNotAllowedResult(IEndpointDetails endpointDetails)
		{
	        _endpointDetails = endpointDetails;
		}

	    public void Update(IResponseDetails responseDetails)
		{
			responseDetails.StatusCode = HttpStatusCode.MethodNotAllowed;
            responseDetails.Allow = _endpointDetails.AllowHeader;
		}
	}
}