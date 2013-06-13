namespace Nonae.Core.Authorization
{
	public class Credentials
	{
		private Credentials(bool isAnonymous, string username, string message)
		{
			IsAnonymous = isAnonymous;
			Username = username;
			Message = message;
		}

		private const string UnsupportedAuthorizationMethod = "Unsupported Authorization Method";
		private const string UserNotFound = "User Not Found";

		public string Username { get; private set;  }
		public bool IsAnonymous { get; private set; }
		public string Message { get; private set; }

		public static Credentials ForAuthenticatedUser(string username)
		{
			return new Credentials(false, username, null);
		}

		public static Credentials ForAnonymousUser()
		{
			return new Credentials(true, null, null);
		}

		public static Credentials ForUnsupportedAuthorizationMethod()
		{
			return new Credentials(false, null, UnsupportedAuthorizationMethod);
		}

		public static Credentials ForUserNotFound()
		{
			return new Credentials(false, null, UserNotFound);
		}
	}
}