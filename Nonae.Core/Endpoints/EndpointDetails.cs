using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
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
		private readonly Func<Credentials, bool> _authorize;
		private readonly Regex _pattern;
		private readonly Dictionary<string, string> _addressParts;
		private readonly IResourceRepository _resourceRepository;

		public EndpointDetails(string url, List<HttpMethod> methods, Func<Credentials, bool> authorize, IResourceRepository resourceRepository, string path)
		{
			_authorize = authorize;
			_methods = methods;
			_pattern = url == null ? null : new Regex(url);
			_resourceRepository = resourceRepository;
			_addressParts = GetAddressParts(path);
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
			get { return _pattern != null; }
		}

		public bool ResourceExists
		{
			get
			{
				return _resourceRepository == null || _resourceRepository.Exists(_addressParts);
			}
		}

		public bool IsAuthorizedFor(Credentials credentials)
		{
			return _authorize(credentials);
		}

		private Dictionary<string, string> GetAddressParts(string path)
		{
			if (path == null) return null;
			if (_pattern == null) return null;
			var match = _pattern.Match(path);

			var addressParts = new Dictionary<string, string>();
			var groupCollection = match.Groups;
			for (var i = 1; i <= groupCollection.Count; i++)
			{
				var key = _pattern.GroupNameFromNumber(i);
				var value = groupCollection[i].Value;
				addressParts.Add(key, value);
			}
			return addressParts;
		}

		public bool Save()
		{
			return _resourceRepository.Save(_addressParts);
		}
	}
}