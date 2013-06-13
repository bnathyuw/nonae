using System.Net.Http;
using System.Web;
using Nonae.Core.Credentials;
using Nonae.Core.Endpoints;

namespace Nonae.Core.Requests
{
	public class RequestDetails : IRequestDetails
	{
		private readonly HttpRequest _request;

		public RequestDetails(HttpRequest request, IEndpoint endpoint, ICredentials credentials)
		{
			_request = request;
			_endpoint = endpoint;
			_credentials = credentials;
			_httpMethod = _request.HttpMethod;
		}

		private readonly IEndpoint _endpoint;
		private readonly ICredentials _credentials;
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
			get { return _credentials.Message == null; }
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
			get { return _credentials.Message; }
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