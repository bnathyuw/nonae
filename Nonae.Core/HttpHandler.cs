using System.Collections.Generic;
using System.Net;
using System.Web;

namespace Nonae.Core
{
	public class HttpHandler : IHttpHandler
	{
		private readonly Dictionary<string, Endpoint> _allow = new Dictionary<string, Endpoint>();

		public void ProcessRequest(HttpContext context)
		{
			var path = context.Request.Path;

			var endpoint = GetBlah(path);

			if (!endpoint.SupportsMethod(context))
				context.Response.StatusCode = (int) HttpStatusCode.MethodNotAllowed;

			SetAllowHeader(context, endpoint);
		}

		private static void SetAllowHeader(HttpContext context, Endpoint endpoint)
		{
			var allowHeader = endpoint.GetAllowHeader();
			context.Response.Headers.Add("Allow", allowHeader);
		}

		private Endpoint GetBlah(string path)
		{
			return _allow[path];
		}

		public bool IsReusable { get { return true; } }

		protected Endpoint AddEndpoint(string url)
		{
			var endpoint = new Endpoint();
			_allow.Add(url, endpoint);
			return endpoint;
		}
	}
}
