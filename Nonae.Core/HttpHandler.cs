using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;

namespace Nonae.Core
{
	public class HttpHandler : IHttpHandler
	{
		private readonly Dictionary<string, Endpoint> _endpoints = new Dictionary<string, Endpoint>();

		public void ProcessRequest(HttpContext context)
		{
			try
			{
				var authorizationHeader = context.Request.Headers["Authorization"];
				if (authorizationHeader != null)
				{
					var bits = authorizationHeader.Split(' ');
					var authorizationType = bits[0];
					if (authorizationType != "Basic")
						throw new UnauthorizedException();
					var encodedCredentials = bits[1];
					var credentialBytes = Convert.FromBase64String(encodedCredentials);
					var credentials = Encoding.Unicode.GetString(credentialBytes);
					if (credentials != "username:password")
						throw new UnauthorizedException();
				}

				var path = context.Request.Path;

				var endpoint = FindEndpoint(path);

				if (endpoint == null)
				{
					if (context.Request.HttpMethod == HttpMethod.Options.ToString())
					{
						context.Response.StatusCode = (int) HttpStatusCode.OK;
						context.Response.Headers.Add("Allow", " ");
					}
					else
						throw new NotFoundException();
				}
				else
				{
					if (!endpoint.SupportsMethod(context))
						throw new MethodNotAllowedException(endpoint);

					SetAllowHeader(context, endpoint);
				}
			}
			catch (FlowException exception)
			{
				exception.Update(context.Response);
			}
		}

		private static void SetAllowHeader(HttpContext context, Endpoint endpoint)
		{
			var allowHeader = endpoint.GetAllowHeader();
			context.Response.Headers.Add("Allow", allowHeader);
		}

		private Endpoint FindEndpoint(string path)
		{
			return _endpoints.ContainsKey(path) ? _endpoints[path] : null;
		}

		public bool IsReusable { get { return true; } }

		protected Endpoint AddEndpoint(string url)
		{
			var endpoint = new Endpoint();
			_endpoints.Add(url, endpoint);
			return endpoint;
		}
	}

	public class UnauthorizedException : FlowException
	{
		public override void Update(HttpResponse response)
		{
			response.StatusCode = (int) HttpStatusCode.Unauthorized;
			response.Headers["WWW-Authenticate"] = "Basic realm=\"foo\"";
		}
	}

	public class MethodNotAllowedException: FlowException{
		private readonly Endpoint _endpoint;

		public MethodNotAllowedException(Endpoint endpoint)
		{
			_endpoint = endpoint;
		}

		public override void Update(HttpResponse response)
		{
			response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
			var allowHeader = _endpoint.GetAllowHeader();
			response.Headers.Add("Allow", allowHeader);
		}
	}

	public class NotFoundException:FlowException
	{
		public override void Update(HttpResponse response)
		{
			response.StatusCode = (int)HttpStatusCode.NotFound;
		}
	}

	public abstract class FlowException : Exception
	{
		public abstract void Update(HttpResponse response);
	}
}
