using System.Collections.Generic;
using System.Linq;

namespace Nonae.Core.Endpoints
{
	public class EndpointStore
	{
		private readonly List<Endpoint> _endpointSpecs = new List<Endpoint>();

		internal EndpointDetails Get(string path)
		{
			var endpointSpec = _endpointSpecs.FirstOrDefault(e => e.IsAt(path));
			return endpointSpec == null ? EndpointDetails.Null() : endpointSpec.GetEndpoint(path);
		}

		public void Add(Endpoint endpoint)
		{
			_endpointSpecs.Add(endpoint);
		}
	}
}