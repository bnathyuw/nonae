using System.Net;
using Nonae.Core.Endpoints;
using Nonae.Core.Responses;

namespace Nonae.Core.Results
{
	public class CreatedResult : IResult
	{
	    private readonly IEndpointDetails _endpointDetails;

	    public CreatedResult(IEndpointDetails endpointDetails)
		{
	        _endpointDetails = endpointDetails;
		}

	    public void Update(IResponseDetails responseDetails)
		{
			responseDetails.StatusCode = HttpStatusCode.Created;
			responseDetails.Allow = _endpointDetails.AllowHeader;

		}
	}
}