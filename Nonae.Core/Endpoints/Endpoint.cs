using System;
using System.Collections.Generic;
using System.Linq;
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

		public Endpoint StoredAt(IResourceRepository resourceRepository)
		{
			_resourceRepository = resourceRepository;
			return this;
		}

		private readonly List<HttpMethod> _methods;
		private Func<Credentials, bool> _authorize;
		private readonly Regex _pattern;
		private Dictionary<string, string> _addressParts;
		private IResourceRepository _resourceRepository;

		private Endpoint(string url)
		{
			_authorize = credentials => true;
			_methods = new List<HttpMethod>();
			_pattern = url == null ? null : new Regex(url);
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

		public Dictionary<string,string> AddressParts
		{
			get
			{
				
				return _addressParts;
			}
		}

		public bool ResourceExists
		{
			get
			{
				return _resourceRepository == null || _resourceRepository.Exists(AddressParts);
			}
		}

		private Dictionary<string, string> GetAddressParts(string path)
		{
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

		public bool IsAuthorizedFor(Credentials credentials)
		{
			return _authorize(credentials);
		}

		public bool IsAt(string path)
		{
			return _pattern != null && _pattern.IsMatch(path);
		}

		public Endpoint At(string path)
		{
			_addressParts = GetAddressParts(path);
			return this;
		}
	}
}