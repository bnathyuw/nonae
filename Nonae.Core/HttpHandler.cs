using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;

namespace Nonae.Core
{
	public class HttpHandler : IHttpHandler
	{
		private readonly Dictionary<string, Endpoint> _endpoints = new Dictionary<string, Endpoint>();

		public void ProcessRequest(HttpContext context)
		{
			var authorizationHeader = context.Request.Headers["Authorization"];
			if (authorizationHeader != null)
			{
				var bits = authorizationHeader.Split(' ');
				var authorizationType = bits[0];
				if (authorizationType != "Basic") context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
				var encodedCredentials = bits[1];
				var credentialBytes = Convert.FromBase64String(encodedCredentials);
				var credentials = Encoding.Unicode.GetString(credentialBytes);
				if (credentials != "username:password") context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
			}

			var path = context.Request.Path;

			var endpoint = FindEndpoint(path);

			if (endpoint == null)
			{
				if (context.Request.HttpMethod == HttpMethod.Options.ToString())
				{
					context.Response.StatusCode = (int) HttpStatusCode.OK;
					context.Response.Headers.Add("Allow", " ");
				}
				else
					context.Response.StatusCode = (int) HttpStatusCode.NotFound;
			}
			else
			{
				if (!endpoint.SupportsMethod(context))
					context.Response.StatusCode = (int) HttpStatusCode.MethodNotAllowed;

				SetAllowHeader(context, endpoint);
			}
		}

		private static void SetAllowHeader(HttpContext context, Endpoint endpoint)
		{
			var allowHeader = endpoint.GetAllowHeader();
			context.Response.Headers.Add("Allow", allowHeader);
		}

		private Endpoint FindEndpoint(string path)
		{
			return _endpoints.ContainsKey(path) ? _endpoints[path] : null;
		}

		public bool IsReusable { get { return true; } }

		protected Endpoint AddEndpoint(string url)
		{
			var endpoint = new Endpoint();
			_endpoints.Add(url, endpoint);
			return endpoint;
		}
	}
}
