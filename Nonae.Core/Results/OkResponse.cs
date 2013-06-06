using System.Web;

namespace Nonae.Core.Results
{
	internal class OkResponse : IResult
	{
		private readonly Endpoint _endpoint;

		public OkResponse(Endpoint endpoint)
		{
			_endpoint = endpoint;
		}

		public void Update(HttpResponse response)
		{
			var allowHeader = _endpoint.GetAllowHeader();
			response.Headers.Add("Allow", allowHeader);
		}
	}
}