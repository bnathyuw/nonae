namespace Nonae.Core.Credentials
{
	public interface ICredentials
	{
		string Username { get; }

		bool IsAnonymous { get; }

		string Message { get; }
	}
}