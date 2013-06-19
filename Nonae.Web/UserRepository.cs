using System.Collections.Generic;
using Nonae.Core.Handlers;

namespace Nonae.Web
{
	public class UserRepository : IResourceRepository
	{
		public bool Exists(Dictionary<string, string> query)
		{
			return query["id"] != "666";
		}
	}
}