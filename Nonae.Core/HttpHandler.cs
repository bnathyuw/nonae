using System.Web;
using Nonae.Core.Endpoints;
using Nonae.Core.Handlers;
using Nonae.Core.Results;

namespace Nonae.Core
{
	public class HttpHandler : IHttpHandler
	{
		private readonly IHandler _authenticationHandler;
		private readonly EndpointStore _endpointStore;

		protected HttpHandler()
		{
			_authenticationHandler = new AuthenticationHandler();
			_endpointStore = new EndpointStore();
		}

		public void ProcessRequest(HttpContext context)
		{
			var endpoint = _endpointStore.Get(context.Request.Path);
			var requestDetails = new RequestDetails(context.Request, endpoint);
			var result = _authenticationHandler.Handle(requestDetails);

			var responseDetails = new ResponseDetails(context.Response);
			result.Update(responseDetails);
		}

		public bool IsReusable
		{
			get { return true; }
		}

		protected Endpoint AddEndpoint(string url)
		{
			return _endpointStore.Add(url);
		}
	}
}