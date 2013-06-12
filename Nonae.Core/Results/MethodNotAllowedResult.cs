using System.Net;
using Nonae.Core.Handlers;

namespace Nonae.Core.Results
{
	public class MethodNotAllowedResult : IResult
	{
		private readonly IRequestDetails _requestDetails;

		public MethodNotAllowedResult(IRequestDetails requestDetails)
		{
			_requestDetails = requestDetails;
		}

		public void Update(ResponseDetails responseDetails)
		{
			responseDetails.StatusCode = HttpStatusCode.MethodNotAllowed;
			responseDetails.Allow = _requestDetails.AllowHeader;
		}
	}
}