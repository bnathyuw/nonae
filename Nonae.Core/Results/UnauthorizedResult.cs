using System.Net;
using System.Web;

namespace Nonae.Core.Results
{
	public class UnauthorizedResult : IResult
	{
		private readonly string _message;

		private UnauthorizedResult(string message)
		{
			_message = message;
		}

		public void Update(HttpResponse response)
		{
			response.StatusCode = (int) HttpStatusCode.Unauthorized;
			response.Headers["WWW-Authenticate"] = "Basic realm=\"foo\"";
			response.Write(_message);
		}

		private const string UnsupportedAuthorizationMethod = "Unsupported Authorization Method";
		private const string InvalidCredentials = "Invalid credentials";

		public static UnauthorizedResult ForInvalidCredentials()
		{
			return new UnauthorizedResult(InvalidCredentials);
		}

		public static UnauthorizedResult ForUnsupportedAuthorizationMethod()
		{
			return new UnauthorizedResult(UnsupportedAuthorizationMethod);
		}
	}
}