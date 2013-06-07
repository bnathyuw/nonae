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

		public void Update(HttpResponse response)
		{
			response.StatusCode = (int) HttpStatusCode.MethodNotAllowed;
			var allowHeader = _requestDetails.AllowHeader;
			response.Headers.Add("Allow", allowHeader);
		}
	}
}