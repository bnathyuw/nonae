namespace Nonae.Core.Authentication
{
	public class AnonymousCredentials : Credentials
	{
		public override bool IsAuthenticated
		{
			get { return true; }
		}

		public override bool AuthorizationMethodIsSupported
		{
			get { return true; }
		}
	}
}