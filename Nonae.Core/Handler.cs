using System.Web;

namespace Nonae.Core
{
	public class Handler : IHttpHandler
	{
		public void ProcessRequest(HttpContext context)
		{
			context.Response.Headers.Add("Allow", "POST");
		}

		public bool IsReusable { get { return true; } }
	}
}
