namespace Nonae.Core.Authorization
{
	public interface IAuthenticationProvider
	{
		bool Authenticate(string username, string password);
	}
}