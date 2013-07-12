using System.Net.Http;
using Nonae.Core.Authorization;
using Nonae.Core.Endpoints;

namespace Nonae.Core.Requests
{
	public class RequestDetails : IRequestDetails
	{
		internal RequestDetails(IEndpointDetails endpointDetails, Credentials credentials, HttpMethod httpMethod)
		{
			_endpointDetails = endpointDetails;
			_credentials = credentials;
			_httpMethod = httpMethod;
		}

		private readonly IEndpointDetails _endpointDetails;
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

	    public string AuthenticationFailureMessage
		{
			get { return _credentials.FailureMessage; }
		}

	    public bool Answers(HttpMethod httpMethod)
		{
			return _httpMethod == httpMethod;
		}

	    public bool IsAuthorized
		{
			get { return _endpointDetails.IsAuthorizedFor(_credentials); }
		}
	}
}