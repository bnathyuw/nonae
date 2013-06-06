using System;
using System.Linq;
using System.Net.Http;

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

		public bool Allows(string httpMethod)
		{
			return _methods.ToList().Contains(new HttpMethod(httpMethod));
		}

		public string GetAllowHeader()
		{
			return String.Join(", ", _methods.ToList());
		}

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