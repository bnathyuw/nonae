namespace Nonae.Core.Authentication
{
	public class InvalidCredentials : Credentials
	{
		public override bool IsAuthenticated
		{
			get { return false; }
		}

		public override bool AuthorizationMethodIsSupported
		{
			get { return false; }
		}

		public override string Username
		{
			get { return ""; }
		}
	}
}