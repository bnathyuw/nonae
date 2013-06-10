using System.Collections.Generic;
using System.Linq;

namespace Nonae.Core.Endpoints
{
	public class EndpointStore
	{
		private readonly List<Endpoint> _endpoints = new List<Endpoint>();

		public IEndpoint Get(string path)
		{
			var endpoint = _endpoints.FirstOrDefault(e => e.IsAt(path));
			return (IEndpoint) endpoint ?? new NullEndpoint();
		}

		public Endpoint Add(string url)
		{
			var endpoint = new Endpoint(url);
			_endpoints.Add(endpoint);
			return endpoint;
		}
	}
}