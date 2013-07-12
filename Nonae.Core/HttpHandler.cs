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
			_handler = HandlerFactory.Build();
			_endpointStore = new EndpointStore();
			_credentialsBuilder = new CredentialsBuilder(authenticationProvider);
		}

	    public void ProcessRequest(HttpContext context)
		{
			var request = context.Request;
			var endpoint = _endpointStore.Get(request.Path);
			var credentials = _credentialsBuilder.From(request.Headers["Authorization"]);
	        var httpMethod = new HttpMethod(request.HttpMethod);
			
			var result = _handler.Handle(endpoint, credentials, httpMethod);

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