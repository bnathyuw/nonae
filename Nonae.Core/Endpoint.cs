using System;
using System.Linq;
using System.Net.Http;
using Nonae.Core.Handlers;

namespace Nonae.Core
{
	public class Endpoint
	{
		private HttpMethod[] _methods;
		private readonly string _url;

		public Endpoint(string url)
		{
			_url = url;
		}

		public bool Allows(RequestDetails requestDetails)
		{
			return _methods.Any(requestDetails.Matches);
		}

		public string GetAllowHeader()
		{
			return String.Join(", ", _methods.ToList());
		}

		public void WithMethods(params HttpMethod[] methods)
		{
			_methods = methods;
		}

		public bool IsAt(RequestDetails requestDetails)
		{
			return requestDetails.Matches(_url);
		}
	}
}