using System.Net;
using Nonae.Core.Requests;
using Nonae.Core.Responses;

namespace Nonae.Core.Results
{
	public class CreatedResult : IResult
	{
		private readonly IRequestDetails _requestDetails;

		public CreatedResult(IRequestDetails requestDetails)
		{
			_requestDetails = requestDetails;
		}

		public void Update(IResponseDetails responseDetails)
		{
			responseDetails.StatusCode = HttpStatusCode.Created;
			responseDetails.Allow = _requestDetails.AllowHeader;

		}
	}
}