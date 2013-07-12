using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Nonae.Core.Authorization;
using Nonae.Core.Handlers;

namespace Nonae.Core.Endpoints
{
	internal class EndpointDetails : IEndpointDetails
	{
		internal static EndpointDetails Null()
		{
			return new EndpointDetails(null, new List<HttpMethod>(), credentials => true, null, null);
		}

		private readonly List<HttpMethod> _methods;
		private readonly Func<ICredentials, bool> _authorize;
		private readonly string _url;
		private readonly Dictionary<string, string> _addressParts;
		private readonly IResourceRepository _resourceRepository;

		public EndpointDetails(string url, List<HttpMethod> methods, Func<ICredentials, bool> authorize, IResourceRepository resourceRepository, string path)
		{
			_authorize = authorize;
			_methods = methods;
			_url = url;
			_resourceRepository = resourceRepository;
			_addressParts = new PathAnalyser(url).GetAddressParts(path);
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

		public bool ResourceExists
		{
			get
			{
				return _resourceRepository == null || _resourceRepository.Exists(_addressParts);
			}
		}

		public bool IsAuthorizedFor(ICredentials credentials)
		{
			return _authorize(credentials);
		}

		public bool Save()
		{
			return _resourceRepository.Save(_addressParts);
		}
	}
}