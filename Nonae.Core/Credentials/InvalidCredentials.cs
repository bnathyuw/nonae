namespace Nonae.Core.Credentials
{
	public class InvalidCredentials : ICredentials
	{
		public bool IsAuthenticated
		{
			get { return false; }
		}

		public bool AuthorizationMethodIsSupported
		{
			get { return false; }
		}

		public string Username
		{
			get { return ""; }
		}

		public bool IsAnonymous
		{
			get { return false; }
		}
	}
}