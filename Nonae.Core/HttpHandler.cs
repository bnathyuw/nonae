using System.Collections.Generic;
using System.Net;
using System.Web;

namespace Nonae.Core
{
	public class HttpHandler : IHttpHandler
	{
		private readonly Dictionary<string, Endpoint> _endpoints = new Dictionary<string, Endpoint>();

		public void ProcessRequest(HttpContext context)
		{
			var path = context.Request.Path;

			var endpoint = FindEndpoint(path);

			if (endpoint == null)
			{
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
