using System.Collections.Generic;
using System.Linq;

namespace Nonae.Core.Endpoints
{
	public class EndpointStore
	{
		private readonly List<Endpoint> _endpoints = new List<Endpoint>();

		public Endpoint Get(string path)
		{
			var endpoint = _endpoints.FirstOrDefault(e => e.IsAt(path));
			return endpoint ?? Endpoint.Null();
		}

		public void Add(Endpoint endpoint)
		{
			_endpoints.Add(endpoint);
		}
	}
}