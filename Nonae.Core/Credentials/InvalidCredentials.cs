namespace Nonae.Core.Credentials
{
	public class InvalidCredentials : ICredentials
	{
		private readonly string _message;

		public InvalidCredentials(string message)
		{
			_message = message;
		}

		public string Username
		{
			get { return null; }
		}

		public bool IsAnonymous
		{
			get { return false; }
		}

		public string Message
		{
			get { return _message; }
		}
	}
}