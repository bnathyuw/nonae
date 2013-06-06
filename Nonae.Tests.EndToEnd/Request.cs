using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Nonae.Tests.EndToEnd
{
	public class Request
	{
		private readonly string _url;
		private readonly HttpMethod _httpMethod;
		private readonly string _entity;
		private readonly WebRequest _webRequest;

		private Request(string url, HttpMethod httpMethod, string entity = null)
		{
			_url = url;
			_httpMethod = httpMethod;
			_entity = entity;
			_webRequest = CreateWebRequest(_url, _entity, _httpMethod);
		}

		public string IfMatch
		{
			set { _webRequest.Headers["If-Match"] = value; }
		}

		public static Request Get(string entityEndpoint)
		{
			return new Request(entityEndpoint, HttpMethod.Get);
		}

		public static Request Head(string entityEndpoint)
		{
			return new Request(entityEndpoint, HttpMethod.Head);
		}

		public static Request Put(string url, string entity)
		{
			return new Request(url, HttpMethod.Put, entity);
		}

		public static Request Delete(string url)
		{
			return new Request(url, HttpMethod.Delete);
		}

		public static Request Post(string url, string entity)
		{
			return new Request(url, HttpMethod.Post, entity);
		}

		public static Request Options(string url)
		{
			return new Request(url, HttpMethod.Options);
		}

		private static WebRequest CreateWebRequest(string url, string entity, HttpMethod httpMethod)
		{
			var webRequest = WebRequest.Create(url);
			webRequest.Method = httpMethod.ToString();
			if (entity != null) WriteBody(webRequest, entity);
			return webRequest;
		}

		private static void WriteBody(WebRequest webRequest, string entity)
		{
			webRequest.ContentLength = entity.Length;
			using (var requestStream = webRequest.GetRequestStream())
			using (var streamWriter = new StreamWriter(requestStream))
				streamWriter.Write(entity);
		}

		public Response GetResponse()
		{
			Console.WriteLine(this);
			var httpWebResponse = GetWebResponse(_webRequest);
			var response = new Response(httpWebResponse);
			Console.WriteLine(response);
			Console.WriteLine();
			return response;
		}

		private static HttpWebResponse GetWebResponse(WebRequest webRequest)
		{
			try
			{
				return (HttpWebResponse) webRequest.GetResponse();
			}
			catch (WebException exception)
			{
				return (HttpWebResponse) exception.Response;
			}
		}

		public override string ToString()
		{
			return string.Format(@"
REQUEST {0:o}

{1} {2}

{3}
{4}", DateTime.UtcNow, _httpMethod, _url, _webRequest.Headers, _entity).Replace("\n", "\n--> ");
		}

		public void SetAuthentication(string authMethod, string username, string password)
		{
			var text = string.Format("{0}:{1}", username, password);
			var bytes = Encoding.Unicode.GetBytes(text);
			var base64String = Convert.ToBase64String(bytes);
			var header = string.Format("{0} {1}", authMethod, base64String);
			_webRequest.Headers["Authorization"] = header;
		}
	}
}