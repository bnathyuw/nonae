using System.Collections.Generic;

namespace Nonae.Core.Handlers
{
	public interface IResourceRepository
	{
		bool Exists(Dictionary<string, string> query);
	}
}