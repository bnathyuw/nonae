using System.Net;
using System.Web;

namespace Nonae.Core.Results
{
	internal class OptionsResult : IResult
	{
		private readonly IEndpoint _endpoint;

		public OptionsResult(IEndpoint endpoint)
		{
			_endpoint = endpoint;
		}

		public void Update(HttpResponse response)
		{
			response.StatusCode = (int) HttpStatusCode.OK;
			response.Headers.Add("Allow", _endpoint.AllowHeader);
		}
	}
}