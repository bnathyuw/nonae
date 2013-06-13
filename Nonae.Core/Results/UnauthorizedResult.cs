using System.Net;
using Nonae.Core.Responses;

namespace Nonae.Core.Results
{
	public class UnauthorizedResult : IResult
	{
		private readonly string _message;

		private UnauthorizedResult(string message)
		{
			_message = message;
		}

		public void Update(ResponseDetails responseDetails)
		{
			responseDetails.StatusCode = HttpStatusCode.Unauthorized;
			responseDetails.WwwAuthenticate = "Basic realm=\"foo\"";
			responseDetails.Body = _message;
		}

		private const string UnsupportedAuthorizationMethod = "Unsupported Authorization Method";
		private const string InvalidCredentials = "Invalid credentials";
		private const string InsufficientPrivileges = "Insufficient privileges";

		public static UnauthorizedResult ForInvalidCredentials()
		{
			return new UnauthorizedResult(InvalidCredentials);
		}

		public static UnauthorizedResult ForUnsupportedAuthorizationMethod()
		{
			return new UnauthorizedResult(UnsupportedAuthorizationMethod);
		}

		public static IResult ForInsufficientPrivileges()
		{
			return new UnauthorizedResult(InsufficientPrivileges);
		}
	}
}