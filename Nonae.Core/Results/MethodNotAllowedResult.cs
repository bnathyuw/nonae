using System.Net;
using Nonae.Core.Requests;
using Nonae.Core.Responses;

namespace Nonae.Core.Results
{
	public class MethodNotAllowedResult : IResult
	{
		private readonly IRequestDetails _requestDetails;

		public MethodNotAllowedResult(IRequestDetails requestDetails)
		{
			_requestDetails = requestDetails;
		}

		public void Update(IResponseDetails responseDetails)
		{
			responseDetails.StatusCode = HttpStatusCode.MethodNotAllowed;
			responseDetails.Allow = _requestDetails.AllowHeader;
		}
	}
}