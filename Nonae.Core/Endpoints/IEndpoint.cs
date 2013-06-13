using Nonae.Core.Handlers;
using Nonae.Core.Requests;

namespace Nonae.Core.Endpoints
{
	public interface IEndpoint
	{
		bool Allows(RequestDetails requestDetails);
		string AllowHeader { get; }
		bool Exists { get; }
		bool IsAuthorizedFor(Credentials.Credentials credentials);
	}
}