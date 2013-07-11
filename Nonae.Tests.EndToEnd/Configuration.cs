using System;
using System.Collections.Generic;

namespace Nonae.Tests.EndToEnd
{
	public static class Configuration
	{
		private static readonly Dictionary<string, string> Urls = new Dictionary<string, string>
			                                                         {
				                                                         {"the root", "http://localhost/nonae"},
				                                                         {"a collection", "http://localhost/nonae/users"},
				                                                         {"a single resource", "http://localhost/nonae/users/1"},
				                                                         {"a silly url", "http://localhost/nonae/i/am/a/teapot"},
				                                                         {"a resource that does not exist", "http://localhost/nonae/users/{0}"},
				                                                         {"a protected resource", "http://localhost/nonae/secrets"}
			                                                         };

		public static readonly Dictionary<string, Func<string, Request>> RequestFactories = new Dictionary<string, Func<string, Request>>
			                                                                                    {
				                                                                                    {"DELETE", Request.Delete},
				                                                                                    {"GET", Request.Get},
				                                                                                    {"HEAD", Request.Head},
				                                                                                    {"OPTIONS", Request.Options},
				                                                                                    {"POST", url => Request.Post(url, "")},
				                                                                                    {"PUT", url => Request.Put(url, "")},
			                                                                                    };

		private static readonly Random Random = new Random(DateTime.Now.Millisecond);

		public static string GetUrl(string resourceKey)
		{
			var url = Urls[resourceKey];
			return url.Contains("{0}") ? string.Format(url, Random.Next()) : url;
		}
	}
}