using System.Web;

namespace Nonae.Core
{
	public class Handler : IHttpHandler
	{
		public void ProcessRequest(HttpContext context)
		{
		}

		public bool IsReusable { get { return true; } }
	}
}
