using System.Net;
using Nonae.Core.Responses;

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