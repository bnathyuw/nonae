using System.Net;

namespace Nonae.Core.Results
{
	internal class OptionsResult : IResult
	{
		private readonly IEndpoint _endpoint;

		public OptionsResult(IEndpoint endpoint)
		{
			_endpoint = endpoint;
		}

		public void Update(ResponseDetails responseDetails)
		{
			responseDetails.StatusCode = HttpStatusCode.OK;
			responseDetails.Allow = _endpoint.AllowHeader;
		}
	}
}