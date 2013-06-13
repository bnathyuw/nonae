using System.Net;
using Nonae.Core.Requests;
using Nonae.Core.Responses;

namespace Nonae.Core.Results
{
	internal class OptionsResult : IResult
	{
		private readonly IRequestDetails _requestDetails;

		public OptionsResult(IRequestDetails requestDetails)
		{
			_requestDetails = requestDetails;
		}

		public void Update(ResponseDetails responseDetails)
		{
			responseDetails.StatusCode = HttpStatusCode.OK;
			responseDetails.Allow = _requestDetails.AllowHeader;
		}
	}
}