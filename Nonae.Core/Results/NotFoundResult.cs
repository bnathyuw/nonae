using System.Net;
using System.Web;

namespace Nonae.Core.Results
{
	public class NotFoundResult : IResult
	{
		public void Update(HttpResponse response)
		{
			response.StatusCode = (int) HttpStatusCode.NotFound;
		}
	}
}