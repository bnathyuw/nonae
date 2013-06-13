using System;
using System.Linq;
using System.Net.Http;
using Nonae.Core.Authorization;
using Nonae.Core.Requests;

namespace Nonae.Core.Endpoints
{
	public class Endpoint : IEndpoint
	{
		private HttpMethod[] _methods;
		private readonly string _url;
		private Func<Credentials, bool> _authorize = credentials => true;

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

		public bool IsAuthorizedFor(Credentials credentials)
		{
			return _authorize(credentials);
		}

		public Endpoint WithMethods(params HttpMethod[] methods)
		{
			_methods = methods;
			return this;
		}

		public bool IsAt(string path)
		{
			return _url == path;
		}

		public void AuthorizedWhen(Func<Credentials, bool> func)
		{
			_authorize = func;
		}
	}
}