using System.Net.Http;
using Nonae.Core.Authorization;

namespace Nonae.Core.Endpoints
{
	public interface IEndpointDetails
	{
		bool Allows(HttpMethod httpMethod);
		string AllowHeader { get; }
		bool Exists { get; }
		bool ResourceExists { get; }
		bool IsAuthorizedFor(ICredentials credentials);
		bool Save();
	}
}