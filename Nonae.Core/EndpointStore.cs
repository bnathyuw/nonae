using System.Collections.Generic;
using System.Linq;

namespace Nonae.Core
{
	static internal class EndpointStore
	{
		private static readonly List<Endpoint> Endpoints = new List<Endpoint>();

		public static Endpoint Get(string path)
		{
			return Endpoints.FirstOrDefault(e => e.IsAt(path));
		}

		public static Endpoint Add(string url)
		{
			var endpoint = new Endpoint(url);
			Endpoints.Add(endpoint);
			return endpoint;
		}
	}
}