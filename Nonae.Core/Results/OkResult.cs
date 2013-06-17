using System.Net;
using Nonae.Core.Requests;
using Nonae.Core.Responses;

namespace Nonae.Core.Results
{
	internal class OkResult : IResult
	{
		private readonly IRequestDetails _requestDetails;

		public OkResult(IRequestDetails requestDetails)
		{
			_requestDetails = requestDetails;
		}

		public void Update(IResponseDetails responseDetails)
		{
			responseDetails.StatusCode = HttpStatusCode.OK;
			responseDetails.Allow = _requestDetails.AllowHeader;
		}
	}
}