using System.Net.Http;

namespace Nonae.Core.Requests
{
	public interface IRequestDetails
	{
		bool HasAuthorization { get; }
		bool IsAuthenticated { get; }
		bool IsAuthorized { get; }
	    bool MethodIsSupported { get; }
	    string AuthenticationFailureMessage { get; }
	    bool Answers(HttpMethod httpMethod);
	}
}