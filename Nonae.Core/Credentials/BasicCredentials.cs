namespace Nonae.Core.Credentials
{
	public class BasicCredentials:ICredentials
	{
		private readonly string _username;

		public BasicCredentials(string username)
		{
			_username = username;
		}

		public string Username
		{
			get { return _username; }
		}

		public bool IsAnonymous
		{
			get { return false; }
		}

		public string Message
		{
			get { return null; }
		}
	}
}