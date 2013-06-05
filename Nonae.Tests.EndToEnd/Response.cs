using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using NUnit.Framework;

namespace Nonae.Tests.EndToEnd
{
	public class Response
	{
		private readonly HttpStatusCode _statusCode;
		private readonly string _body;
		private readonly WebHeaderCollection _headers;
		private IEnumerable<string> _allow;

		public Response(HttpWebResponse httpWebResponse)
		{
			_statusCode = httpWebResponse.StatusCode;
			_body = ReadBody(httpWebResponse);
			_headers = httpWebResponse.Headers;
		}

		public HttpStatusCode StatusCode
		{
			get { return _statusCode; }
		}

		public string Body
		{
			get { return _body; }
		}

		public string Location
		{
			get { return _headers["Location"]; }
		}

		public IEnumerable<string> Allow
		{
			get { return _allow ?? (_allow = GetHeaderValues("Allow")); }
		}

		private IEnumerable<string> GetHeaderValues(string headerName)
		{
			return _headers[headerName].Split(new[] {',', ' '}, StringSplitOptions.RemoveEmptyEntries);
		}

		public string ETag
		{
			get { return _headers["ETag"]; }
		}

		public object WwwAuthenticate
		{
			get { return _headers["WWW-Authenticate"]; }
		}

		private static string ReadBody(WebResponse webResponse)
		{
			using (var responseStream = webResponse.GetResponseStream())
			{
				Assert.IsNotNull(responseStream);
				using (var streamReader = new StreamReader(responseStream))
					return streamReader.ReadToEnd();
			}
		}

		public override string ToString()
		{
			return string.Format(@"
RESPONSE {0:o}

HTTP 1.1 {1} {2}

{3}
{4}",  DateTime.UtcNow, (int) _statusCode, _statusCode, _headers, _body).Replace("\n", "\n--> ");
		}
	}
}