using System;
using System.Linq;
using System.Net.Http;
using Nonae.Core.Handlers;

namespace Nonae.Core.Endpoints
{
	public class Endpoint : IEndpoint
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

		public string AllowHeader
		{
			get { return String.Join(", ", _methods.ToList()); }
		}

		public bool Exists { get { return true; } }

		public void WithMethods(params HttpMethod[] methods)
		{
			_methods = methods;
		}

		public bool IsAt(string path)
		{
			return _url == path;
		}
	}
}