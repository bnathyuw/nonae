using System.Net;
using System.Web;

namespace Nonae.Core.Results
{
	internal class OptionsResult : IResult
	{
		private readonly Endpoint _endpoint;

		public OptionsResult(Endpoint endpoint)
		{
			_endpoint = endpoint;
		}

		public void Update(HttpResponse response)
		{
			response.StatusCode = (int) HttpStatusCode.OK;
			if (_endpoint == null)
			{
				response.Headers.Add("Allow", " ");
			}
			else
			{
				response.StatusCode = (int) HttpStatusCode.OK;
				var allowHeader = _endpoint.GetAllowHeader();
				response.Headers.Add("Allow", allowHeader);
			}
		}
	}
}