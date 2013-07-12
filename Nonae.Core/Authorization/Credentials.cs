namespace Nonae.Core.Authorization
{
	public class Credentials : ICredentials
	{
		public Credentials(bool isAnonymous, string username, string message)
		{
			IsAnonymous = isAnonymous;
			Username = username;
			FailureMessage = message;
		}

		public string Username { get; private set;  }
		public bool IsAnonymous { get; private set; }
		public string FailureMessage { get; private set; }

		public bool IsAuthenticated
		{
			get { return FailureMessage == null; }
		}
	}
}