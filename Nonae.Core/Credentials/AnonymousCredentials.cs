namespace Nonae.Core.Credentials
{
	public class AnonymousCredentials : ICredentials
	{
		public bool IsAuthenticated
		{
			get { return true; }
		}

		public bool AuthorizationMethodIsSupported
		{
			get { return true; }
		}

		public string Username
		{
			get { return null; }
		}

		public bool IsAnonymous
		{
			get { return true; }
		}
	}
}