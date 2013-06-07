using System.Web;
using Nonae.Core.Handlers;

namespace Nonae.Core.Results
{
	internal class OkResponse : IResult
	{
		private readonly RequestDetails _requestDetails;

		public OkResponse(RequestDetails requestDetails)
		{
			_requestDetails = requestDetails;
		}

		public void Update(HttpResponse response)
		{
			var allowHeader = _requestDetails.AllowHeader;
			response.Headers.Add("Allow", allowHeader);
		}
	}
}