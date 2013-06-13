using Nonae.Core.Requests;

namespace Nonae.Core.Endpoints
{
	internal class NullEndpoint : IEndpoint
	{
		public bool Allows(RequestDetails requestDetails)
		{
			return false;
		}

		public string AllowHeader { get { return " "; } }

		public bool Exists { get { return false; } }

		public bool IsAuthorizedFor(Credentials.ICredentials credentials)
		{
			return true;
		}
	}
}