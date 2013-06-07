using System.Net;
using System.Web;

namespace Nonae.Core.Results
{
	public class ResponseDetails
	{
		private readonly HttpResponse _response;

		public ResponseDetails(HttpResponse response)
		{
			_response = response;
		}

		public HttpStatusCode StatusCode
		{
			set { _response.StatusCode = (int) value; }
		}

		public string WwwAuthenticate
		{
			set { _response.Headers["WWW-Authenticate"] = value; }
		}

		public string Body
		{
			set { _response.Write(value); }
		}

		public string Allow
		{
			set { _response.Headers.Add("Allow", value); }
		}
	}

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