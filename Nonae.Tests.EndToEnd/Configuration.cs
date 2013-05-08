using System;
using System.Collections.Generic;

namespace Nonae.Tests.EndToEnd
{
	public static class Configuration
	{
		public static readonly Dictionary<string, string> Urls = new Dictionary<string, string>
			                                                         {
				                                                         {"the root", "http://localhost/nonae"},
				                                                         {"a collection", "http://localhost/nonae/users"},
				                                                         {"a single resource", "http://localhost/nonae/users/1"}
			                                                         };

		public static readonly Dictionary<string, Func<string, Request>> RequestFactories = new Dictionary<string, Func<string, Request>>
			                                                                                    {
				                                                                                    {"GET", Request.Get},
				                                                                                    {"OPTIONS", Request.Options}
			                                                                                    };
	}
}