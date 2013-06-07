using System.Net;
using System.Web;

namespace Nonae.Core.Results
{
	public class NotFoundResult : IResult
	{
		public void Update(ResponseDetails responseDetails)
		{
			responseDetails.StatusCode = HttpStatusCode.NotFound;
		}
	}
}