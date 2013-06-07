using System.Net.Http;
using System.Web;

namespace Nonae.Core.Handlers
{
	public class RequestDetails
	{
		private readonly HttpRequest _request;

		public RequestDetails(HttpRequest request)
		{
			_request = request;
			_endpoint = EndpointStore.Get(this);
			_authorizationHeader = _request.Headers["Authorization"];
			_credentials = Credentials.From(_authorizationHeader);
			_httpMethod = _request.HttpMethod;
		}

		private readonly IEndpoint _endpoint;
		private readonly string _authorizationHeader;
		private readonly Credentials _credentials;
		private readonly string _httpMethod;

		public bool Matches(string url)
		{
			return url == _request.Path;
		}

		public bool Matches(HttpMethod options)
		{
			return _httpMethod == options.ToString();
		}

		public bool HasAuthorization
		{
			get { return _authorizationHeader == null; }
		}

		public bool AuthorizationMethodIsSupported
		{
			get { return _credentials.AuthorizationMethodIsSupported; }
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
	}
}