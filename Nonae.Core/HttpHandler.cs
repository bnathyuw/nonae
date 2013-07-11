using System.Net.Http;
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
			IHandler notFoundHandler = new NotFoundHandler();
			var methodIsSupportedHandler = new MethodIsSupportedHandler(okHandler);
			var resourceExistsHandler = new ResourceExistsHandler(methodIsSupportedHandler, notFoundHandler);
			var endpointExistsHandler = new EndpointExistsHandler(resourceExistsHandler);
			var optionsHandler = new OptionsHandler(endpointExistsHandler);
			var authorizationHandler = new AuthorizationHandler(optionsHandler);
			var authenticationHandler = new AuthenticationHandler(authorizationHandler);
			return authenticationHandler;
		}

		public void ProcessRequest(HttpContext context)
		{
			var request = context.Request;
			var endpoint = _endpointStore.Get(request.Path);
			var credentials = _credentialsBuilder.From(request.Headers["Authorization"]);
			var requestDetails = new RequestDetails(endpoint, credentials, new HttpMethod(request.HttpMethod));
			
			var result = _handler.Handle(requestDetails);

			var responseDetails = new ResponseDetails(context.Response);
			result.Update(responseDetails);
		}

		public bool IsReusable
		{
			get { return true; }
		}

		protected void Add(Endpoint endpoint)
		{
			_endpointStore.Add(endpoint);
		}
	}
}