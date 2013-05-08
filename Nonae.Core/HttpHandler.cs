using System.Collections.Generic;
using System.Web;

namespace Nonae.Core
{
	public class HttpHandler : IHttpHandler
	{
		protected Dictionary<string, string> _allow = new Dictionary<string, string>();

		public void ProcessRequest(HttpContext context)
		{
			context.Response.Headers.Add("Allow", GetAllow(context));
		}

		private string GetAllow(HttpContext context)
		{
			var path = context.Request.Path;
			return _allow[path];
		}

		public bool IsReusable { get { return true; } }
	}
}
