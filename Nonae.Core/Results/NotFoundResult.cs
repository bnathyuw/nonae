using System.Net;
using Nonae.Core.Responses;

namespace Nonae.Core.Results
{
	public class NotFoundResult : IResult
	{
		private const string UnknownAddress = "Unknown Address";
		private readonly string _message;

		public NotFoundResult(string message)
		{
			_message = message;
		}

		public void Update(IResponseDetails responseDetails)
		{
			responseDetails.StatusCode = HttpStatusCode.NotFound;
			responseDetails.Body = _message;
		}

		public static NotFoundResult ForUnknownAddress()
		{
			return new NotFoundResult(UnknownAddress);
		}
	}
}