using System.Collections.Specialized;
using System.Net.Http;

namespace Nonae.Core.Handlers
{
	public class RequestDetails
	{
		private readonly string _path;
		private readonly string _httpMethod;
		private readonly NameValueCollection _headers;

		public RequestDetails(string path, string httpMethod, NameValueCollection headers)
		{
			_path = path;
			_httpMethod = httpMethod;
			_headers = headers;
		}

		public NameValueCollection Headers
		{
			get { return _headers; }
		}

		public Endpoint Endpoint { get; set; }

		public bool Matches(string url)
		{
			return url == _path;
		}

		public bool Matches(HttpMethod options)
		{
			return _httpMethod == options.ToString();
		}
	}
}