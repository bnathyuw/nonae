using System.Net;
using System.Web;

namespace Nonae.Core.Results
{
	public class MethodNotAllowedResult : IResult
	{
		private readonly Endpoint _endpoint;

		public MethodNotAllowedResult(Endpoint endpoint)
		{
			_endpoint = endpoint;
		}

		public void Update(HttpResponse response)
		{
			response.StatusCode = (int) HttpStatusCode.MethodNotAllowed;
			var allowHeader = _endpoint.GetAllowHeader();
			response.Headers.Add("Allow", allowHeader);
		}
	}
}