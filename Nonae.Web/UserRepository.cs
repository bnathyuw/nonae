﻿using System.Collections.Generic;
using System.Linq;
using Nonae.Core.Handlers;

namespace Nonae.Web
{
	public class UserRepository : IResourceRepository
	{
		private static readonly IEnumerable<int> Users;

		static UserRepository()
		{
			Users = new List<int> {1,2,3,4};
		}

		public bool Exists(Dictionary<string, string> query)
		{
			var userId = int.Parse(query["id"]);
			return Users.Any(u => u == userId);
		}
	}
}