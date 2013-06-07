using System.Web;
using Nonae.Core.Handlers;

namespace Nonae.Core
{
	public class HttpHandler : IHttpHandler
	{
		private readonly IHandler _authenticationHandler;

		protected HttpHandler()
		{
			_authenticationHandler = new AuthenticationHandler();
		}

		public void ProcessRequest(HttpContext context)
		{
			var requestDetails = new RequestDetails(context.Request);
			var result = _authenticationHandler.Handle(requestDetails);
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