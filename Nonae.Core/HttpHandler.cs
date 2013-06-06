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
			var exception = GetValue(context);
			exception.Update(context.Response);
		}

		private IResult GetValue(HttpContext context)
		{
			var authorizationHeader = context.Request.Headers["Authorization"];
			if (authorizationHeader != null)
			{
				var bits = authorizationHeader.Split(' ');
				var authorizationType = bits[0];
				if (authorizationType != "Basic")
					return UnauthorizedResult.ForUnsupportedAuthorizationMethod();
				var encodedCredentials = bits[1];
				var credentialBytes = Convert.FromBase64String(encodedCredentials);
				var credentials = Encoding.Unicode.GetString(credentialBytes);
				if (credentials != "username:password")
					return UnauthorizedResult.ForInvalidCredentials();
			}

			// TODO: Authorize against endpoint?

			var path = context.Request.Path;

			if (IsOptions(context))
			{
				return DoOptions(context, path);
			}

			if (!_endpoints.ContainsKey(path))
				return new NotFoundResult();

			var endpoint = _endpoints[path];

			if (!endpoint.SupportsMethod(context))
				return new MethodNotAllowedResult(endpoint);
			
			return new OkResponse(endpoint);
		}

		private static bool IsOptions(HttpContext context)
		{
			return context.Request.HttpMethod == HttpMethod.Options.ToString();
		}

		private OptionsResult DoOptions(HttpContext context, string path)
		{
			context.Response.StatusCode = (int) HttpStatusCode.OK;

			if (!_endpoints.ContainsKey(path))
				return new OptionsResult(" ");
			var endpoint = _endpoints[path];
			var allow = endpoint.GetAllowHeader();
			return new OptionsResult(allow);
		}

		public bool IsReusable
		{
			get { return true; }
		}

		protected Endpoint AddEndpoint(string url)
		{
			var endpoint = new Endpoint();
			_endpoints.Add(url, endpoint);
			return endpoint;
		}
	}

	internal class OptionsResult : IResult
	{
		private readonly string _allow;

		public OptionsResult(string allow)
		{
			_allow = allow;
		}

		public void Update(HttpResponse response)
		{
			response.StatusCode = (int) HttpStatusCode.OK;
			response.Headers.Add("Allow", _allow);
		}
	}

	internal class OkResponse : IResult
	{
		private readonly Endpoint _endpoint;

		public OkResponse(Endpoint endpoint)
		{
			_endpoint = endpoint;
		}

		public void Update(HttpResponse response)
		{
			var allowHeader = _endpoint.GetAllowHeader();
			response.Headers.Add("Allow", allowHeader);
		}
	}

	public class UnauthorizedResult : IResult
	{
		private readonly string _message;

		private UnauthorizedResult(string message)
		{
			_message = message;
		}

		public void Update(HttpResponse response)
		{
			response.StatusCode = (int) HttpStatusCode.Unauthorized;
			response.Headers["WWW-Authenticate"] = "Basic realm=\"foo\"";
			response.Write(_message);
		}

		private const string UnsupportedAuthorizationMethod = "Unsupported Authorization Method";
		private const string InvalidCredentials = "Invalid credentials";

		public static UnauthorizedResult ForInvalidCredentials()
		{
			return new UnauthorizedResult(InvalidCredentials);
		}

		public static UnauthorizedResult ForUnsupportedAuthorizationMethod()
		{
			return new UnauthorizedResult(UnsupportedAuthorizationMethod);
		}
	}

	public class MethodNotAllowedResult : IResult
	{
		private readonly Endpoint _endpoint;

		public MethodNotAllowedResult(Endpoint endpoint)
		{
			_endpoint = endpoint;
		}

		public void Update(HttpResponse response)
		{
			response.StatusCode = (int) HttpStatusCode.MethodNotAllowed;
			var allowHeader = _endpoint.GetAllowHeader();
			response.Headers.Add("Allow", allowHeader);
		}
	}

	public class NotFoundResult : IResult
	{
		public void Update(HttpResponse response)
		{
			response.StatusCode = (int) HttpStatusCode.NotFound;
		}
	}

	public interface IResult
	{
		void Update(HttpResponse response);
	}
}