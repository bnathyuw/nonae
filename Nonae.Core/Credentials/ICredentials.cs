namespace Nonae.Core.Credentials
{
	public interface ICredentials
	{
		bool IsAuthenticated { get; }

		bool AuthorizationMethodIsSupported { get; }

		string Username { get; }

		bool IsAnonymous { get; }
	}
}