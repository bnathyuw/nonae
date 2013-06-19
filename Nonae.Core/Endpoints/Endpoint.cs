using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using Nonae.Core.Authorization;
using Nonae.Core.Handlers;

namespace Nonae.Core.Endpoints
{
	public class Endpoint
	{
		public static Endpoint AtUrl(string url)
		{
			return new Endpoint(url);
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

		public Endpoint StoredAt(IResourceRepository resourceRepository)
		{
			_resourceRepository = resourceRepository;
			return this;
		}

		private Endpoint(string url)
		{
			_url = url;
		}

		private readonly List<HttpMethod> _methods = new List<HttpMethod>();
		private Func<Credentials, bool> _authorize = credentials => true;
		private IResourceRepository _resourceRepository;
		private readonly string _url;

		public bool IsAt(string path)
		{
			return new Regex(_url).IsMatch(path);
		}
		
		internal EndpointDetails GetEndpointDetails(string path)
		{
			return new EndpointDetails(_url, _methods, _authorize, _resourceRepository, path);
		}
	}
}