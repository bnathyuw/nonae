namespace Nonae.Core.Credentials
{
	public interface IAuthenticationProvider
	{
		bool Authenticate(string username, string password);
	}
}