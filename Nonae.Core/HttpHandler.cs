using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nonae.Core.Handlers;

namespace Nonae.Core
{
	public class HttpHandler : IHttpHandler
	{
		private static readonly Dictionary<string, Endpoint> Endpoints = new Dictionary<string, Endpoint>();
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

		public static Endpoint GetEndpoint(string path)
		{
			return Endpoints.FirstOrDefault(e => e.Key == path).Value;
		}

		public bool IsReusable
		{
			get { return true; }
		}

		protected static Endpoint AddEndpoint(string url)
		{
			var endpoint = new Endpoint();
			Endpoints.Add(url, endpoint);
			return endpoint;
		}
	}
}