using System.Web;
using Nonae.Core.Handlers;

namespace Nonae.Core
{
	public class HttpHandler : IHttpHandler
	{
		private readonly AuthenticationHandler _authenticationHandler;

		protected HttpHandler()
		{
			_authenticationHandler = new AuthenticationHandler();
		}

		public void ProcessRequest(HttpContext context)
		{
			var result = _authenticationHandler.CheckAuthentication(context);
			result.Update(context.Response);
		}

		public bool IsReusable
		{
			get { return true; }
		}

		protected static Endpoint AddEndpoint(string url)
		{
			return EndpointStore.Add(url);
		}
	}
}