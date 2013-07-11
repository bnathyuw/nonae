using System.Net.Http;

namespace Nonae.Core.Requests
{
	public interface IRequestDetails
	{
		bool HasAuthorization { get; }
		bool IsAuthenticated { get; }
		bool IsAuthorized { get; }
		bool EndpointExists { get; }
		bool MethodIsSupported { get; }
		string AllowHeader { get; }
		string AuthenticationFailureMessage { get; }
		bool ResourceExists { get; }
		bool Answers(HttpMethod httpMethod);
	}
}