using Nonae.Core.Handlers;
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

		public void Update(ResponseDetails responseDetails)
		{
			responseDetails.Allow = _requestDetails.AllowHeader;
		}
	}
}