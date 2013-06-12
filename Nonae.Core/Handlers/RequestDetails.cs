using System.Net.Http;
using System.Web;
using Nonae.Core.Authentication;
using Nonae.Core.Endpoints;

namespace Nonae.Core.Handlers
{
	public interface IRequestDetails
	{
		bool HasAuthorization { get; }
		bool AuthorizationMethodIsSupported { get; }
		bool IsAuthenticated { get; }
		bool IsAuthorized { get; }
		bool EndpointExists { get; }
		bool MethodIsSupported { get; }
		bool IsOptionsRequest { get; }
		string AllowHeader { get; }
	}

	public class RequestDetails : IRequestDetails
	{
		private readonly HttpRequest _request;

		public RequestDetails(HttpRequest request, IEndpoint endpoint)
		{
			_request = request;
			_endpoint = endpoint;
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
			get { return _authorizationHeader != null; }
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