using System;
using System.Linq;
using System.Net.Http;
using Nonae.Core.Handlers;
using Nonae.Core.Requests;

namespace Nonae.Core.Endpoints
{
	public class Endpoint : IEndpoint
	{
		private HttpMethod[] _methods;
		private readonly string _url;
		private Func<Credentials.ICredentials, bool> _authorize = credentials => true;

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

		public bool IsAuthorizedFor(Credentials.ICredentials credentials)
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

		public Endpoint AuthorizedWhen(Func<Credentials.ICredentials, bool> func)
		{
			_authorize = func;
			return this;
		}
	}
}