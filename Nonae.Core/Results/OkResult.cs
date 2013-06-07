using Nonae.Core.Handlers;

namespace Nonae.Core.Results
{
	internal class OkResult : IResult
	{
		private readonly RequestDetails _requestDetails;

		public OkResult(RequestDetails requestDetails)
		{
			_requestDetails = requestDetails;
		}

		public void Update(ResponseDetails responseDetails)
		{
			responseDetails.Allow = _requestDetails.AllowHeader;
		}
	}
}