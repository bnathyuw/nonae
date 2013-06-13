namespace Nonae.Core.Requests
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
}