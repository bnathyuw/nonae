using System.Collections.Generic;
using System.Linq;
using Nonae.Core.Handlers;

namespace Nonae.Core.Endpoints
{
	static internal class EndpointStore
	{
		private static readonly List<Endpoint> Endpoints = new List<Endpoint>();

		public static IEndpoint Get(RequestDetails requestDetails)
		{
			var endpoint = Endpoints.FirstOrDefault(e => e.IsAt(requestDetails));
			return (IEndpoint) endpoint ?? new NullEndpoint();
		}

		public static Endpoint Add(string url)
		{
			var endpoint = new Endpoint(url);
			Endpoints.Add(endpoint);
			return endpoint;
		}
	}
}