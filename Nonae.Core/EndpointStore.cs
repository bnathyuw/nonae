using System.Collections.Generic;
using System.Linq;
using Nonae.Core.Handlers;

namespace Nonae.Core
{
	static internal class EndpointStore
	{
		private static readonly List<Endpoint> Endpoints = new List<Endpoint>();

		public static Endpoint Get(RequestDetails requestDetails)
		{
			return Endpoints.FirstOrDefault(e => e.IsAt(requestDetails));
		}

		public static Endpoint Add(string url)
		{
			var endpoint = new Endpoint(url);
			Endpoints.Add(endpoint);
			return endpoint;
		}
	}
}