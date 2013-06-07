using System.Net;
using System.Web;
using Nonae.Core.Handlers;

namespace Nonae.Core.Results
{
	public class MethodNotAllowedResult : IResult
	{
		private readonly RequestDetails _requestDetails;

		public MethodNotAllowedResult(RequestDetails requestDetails)
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