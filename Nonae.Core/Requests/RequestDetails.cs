using System.Net.Http;
using Nonae.Core.Authorization;
using Nonae.Core.Endpoints;

namespace Nonae.Core.Requests
{
	public class RequestDetails : IRequestDetails
	{
		public RequestDetails(Endpoint endpoint, Credentials credentials, HttpMethod httpMethod)
		{
			_endpoint = endpoint;
			_credentials = credentials;
			_httpMethod = httpMethod;
		}

		private readonly Endpoint _endpoint;
		private readonly Credentials _credentials;
		private readonly HttpMethod _httpMethod;

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
			get { return _endpoint.Allows(_httpMethod); }
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

		public bool ResourceExists
		{
			get { return _endpoint.ResourceExists; }
		}

		public bool IsOptionsRequest
		{
			get { return _httpMethod == HttpMethod.Options; }
		}

		public bool IsAuthorized
		{
			get { return _endpoint.IsAuthorizedFor(_credentials); }
		}
	}
}