namespace Nonae.Core.Credentials
{
	public class AnonymousCredentials : ICredentials
	{
		public string Username
		{
			get { return null; }
		}

		public bool IsAnonymous
		{
			get { return true; }
		}

		public string Message
		{
			get { return null; }
		}
	}
}