using System.Net.Http;
using System.Web;
using Nonae.Core.Authorization;
using Nonae.Core.Endpoints;

namespace Nonae.Core.Requests
{
	public class RequestDetails : IRequestDetails
	{
		private readonly HttpRequest _request;

		public RequestDetails(HttpRequest request, Endpoint endpoint, Credentials credentials)
		{
			_request = request;
			_endpoint = endpoint;
			_credentials = credentials;
			_httpMethod = _request.HttpMethod;
		}

		private readonly Endpoint _endpoint;
		private readonly Credentials _credentials;
		private readonly string _httpMethod;

		public bool Matches(HttpMethod options)
		{
			return _httpMethod == options.ToString();
		}

		public bool HasAuthorization
		{
			get { return !_credentials.IsAnonymous; }
		}

		public bool IsAuthenticated
		{
			get { return _credentials.IsAuthenticated; }
		}

		public bool MethodIsSupported
		{
			get { return _endpoint.Allows(this); }
		}

		public bool EndpointExists
		{
			get { return _endpoint.Exists; }
		}

		public string AllowHeader
		{
			get { return _endpoint.AllowHeader; }
		}

		public string AuthenticationFailureMessage
		{
			get { return _credentials.FailureMessage; }
		}

		public bool IsOptionsRequest
		{
			get { return Matches(HttpMethod.Options); }
		}

		public bool IsAuthorized
		{
			get { return _endpoint.IsAuthorizedFor(_credentials); }
		}
	}
}