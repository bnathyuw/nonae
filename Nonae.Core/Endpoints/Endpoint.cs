using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Nonae.Core.Authorization;

namespace Nonae.Core.Endpoints
{
	public class Endpoint
	{
		public static Endpoint AtUrl(string url)
		{
			return new Endpoint(url);
		}

		internal static Endpoint Null()
		{
			return new Endpoint(null);
		}

		public Endpoint WithMethods(params HttpMethod[] methods)
		{
			_methods.AddRange(methods);
			return this;
		}

		public Endpoint AuthorizedWhen(Func<Credentials, bool> func)
		{
			_authorize = func;
			return this;
		}

		private readonly List<HttpMethod> _methods;
		private readonly string _url;
		private Func<Credentials, bool> _authorize;

		private Endpoint(string url)
		{
			_authorize = credentials => true;
			_methods = new List<HttpMethod>();
			_url = url;
		}

		public bool Allows(HttpMethod httpMethod)
		{
			return _methods.Any(method => method == httpMethod);
		}

		public string AllowHeader
		{
			get { return String.Join(", ", _methods.ToList()); }
		}

		public bool Exists
		{
			get { return _url != null; }
		}

		public bool IsAuthorizedFor(Credentials credentials)
		{
			return _authorize(credentials);
		}

		public bool IsAt(string path)
		{
			return _url == path;
		}
	}
}