using System.Web;

namespace Nonae.Core
{
	public class Handler : IHttpHandler
	{
		public void ProcessRequest(HttpContext context)
		{
			context.Response.Headers.Add("Allow", GetAllow(context));
		}

		private static string GetAllow(HttpContext context)
		{
			return context.Request.Path == "/nonae/users" ? "POST" : "GET, PUT, HEAD, DELETE";
		}

		public bool IsReusable { get { return true; } }
	}
}
