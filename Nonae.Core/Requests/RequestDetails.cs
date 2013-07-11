using System.Net.Http;
using Nonae.Core.Authorization;
using Nonae.Core.Endpoints;

namespace Nonae.Core.Requests
{
	public class RequestDetails : IRequestDetails
	{
		internal RequestDetails(EndpointDetails endpointDetails, Credentials credentials, HttpMethod httpMethod)
		{
			_endpointDetails = endpointDetails;
			_credentials = credentials;
			_httpMethod = httpMethod;
		}

		private readonly EndpointDetails _endpointDetails;
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
			get { return _endpointDetails.Allows(_httpMethod); }
		}

		public bool EndpointExists
		{
			get { return _endpointDetails.Exists; }
		}

		public string AllowHeader
		{
			get { return _endpointDetails.AllowHeader; }
		}

		public string AuthenticationFailureMessage
		{
			get { return _credentials.FailureMessage; }
		}

		public bool ResourceExists
		{
			get { return _endpointDetails.ResourceExists; }
		}

		public bool IsPutRequest
		{
			get { return _httpMethod == HttpMethod.Put; }
		}

		public bool IsOptionsRequest
		{
			get { return _httpMethod == HttpMethod.Options; }
		}

		public bool IsAuthorized
		{
			get { return _endpointDetails.IsAuthorizedFor(_credentials); }
		}
	}
}