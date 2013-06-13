using System.Web;
using Nonae.Core.Authorization;
using Nonae.Core.Endpoints;
using Nonae.Core.Handlers;
using Nonae.Core.Requests;
using Nonae.Core.Responses;

namespace Nonae.Core
{
	public class HttpHandler : IHttpHandler
	{
		private readonly IHandler _handler;
		private readonly EndpointStore _endpointStore;
		private readonly CredentialsBuilder _credentialsBuilder;

		protected HttpHandler(IAuthenticationProvider authenticationProvider)
		{
			_handler = GetHandler();
			_endpointStore = new EndpointStore();
			_credentialsBuilder = new CredentialsBuilder(authenticationProvider);
		}

		private static AuthenticationHandler GetHandler()
		{
			var okHandler = new OkHandler();
			var methodIsSupportedHandler = new MethodIsSupportedHandler(okHandler);
			var endpointExistsHandler = new EndpointExistsHandler(methodIsSupportedHandler);
			var optionsHandler = new OptionsHandler(endpointExistsHandler);
			var authorizationHandler = new AuthorizationHandler(optionsHandler);
			var authenticationHandler = new AuthenticationHandler(authorizationHandler);
			return authenticationHandler;
		}

		public void ProcessRequest(HttpContext context)
		{
			var endpoint = _endpointStore.Get(context.Request.Path);
			var request = context.Request;
			var credentials = _credentialsBuilder.From(request.Headers["Authorization"]);
			var requestDetails = new RequestDetails(request, endpoint, credentials);
			var result = _handler.Handle(requestDetails);

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