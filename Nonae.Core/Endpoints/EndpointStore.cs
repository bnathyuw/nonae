using System.Collections.Generic;
using System.Linq;

namespace Nonae.Core.Endpoints
{
	public class EndpointStore
	{
		private readonly List<Endpoint> _endpoints = new List<Endpoint>();

		internal EndpointDetails Get(string path)
		{
			var endpoint = _endpoints.FirstOrDefault(e => e.IsAt(path));
			return endpoint == null ? EndpointDetails.Null() : endpoint.GetEndpointDetails(path);
		}

		public void Add(Endpoint endpoint)
		{
			_endpoints.Add(endpoint);
		}
	}
}