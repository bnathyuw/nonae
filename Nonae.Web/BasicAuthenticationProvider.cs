using System.Collections.Generic;
using System.Linq;
using Nonae.Core.Authorization;

namespace Nonae.Web
{
	public class BasicAuthenticationProvider : IAuthenticationProvider
	{
		private readonly IEnumerable<string> _users = new List<string> {"username:password", "admin:password"};

		public bool Authenticate(string username, string password)
		{
			return _users.Contains(username + ":" + password);
		}
	}
}