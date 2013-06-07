using Nonae.Core.Handlers;

namespace Nonae.Core
{
	internal class NullEndpoint : IEndpoint
	{
		public bool Allows(RequestDetails requestDetails)
		{
			return false;
		}

		public string AllowHeader { get { return " "; } }

		public bool Exists { get { return false; } }
	}
}