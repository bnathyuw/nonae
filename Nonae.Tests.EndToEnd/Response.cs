using System;
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

		public string Allow
		{
			get { return _headers["Allow"]; }
		}

		public string ETag
		{
			get { return _headers["ETag"]; }
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