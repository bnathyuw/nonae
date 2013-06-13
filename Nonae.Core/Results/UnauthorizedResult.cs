using System.Net;
using Nonae.Core.Responses;

namespace Nonae.Core.Results
{
	public class UnauthorizedResult : IResult
	{
		private readonly string _message;

		public UnauthorizedResult(string message)
		{
			_message = message;
		}

		public void Update(ResponseDetails responseDetails)
		{
			responseDetails.StatusCode = HttpStatusCode.Unauthorized;
			responseDetails.WwwAuthenticate = "Basic realm=\"foo\"";
			responseDetails.Body = _message;
		}

		private const string InsufficientPrivileges = "Insufficient privileges";

		public static IResult ForInsufficientPrivileges()
		{
			return new UnauthorizedResult(InsufficientPrivileges);
		}
	}
}