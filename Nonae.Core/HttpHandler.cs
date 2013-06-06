using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;

namespace Nonae.Core
{
	public class HttpHandler : IHttpHandler
	{
		private static readonly Dictionary<string, Endpoint> Endpoints = new Dictionary<string, Endpoint>();

		public void ProcessRequest(HttpContext context)
		{
			var result = GetResult(context);
			result.Update(context.Response);
		}

		private static IResult GetResult(HttpContext context)
		{
			var authorizationHeader = context.Request.Headers["Authorization"];
			return authorizationHeader != null ? 
				CheckAuthentication(context, authorizationHeader) : 
				CheckIsOptions(context, context.Request.Path);
		}

		private static IResult CheckAuthentication(HttpContext context, string authorizationHeader)
		{
			var bits = authorizationHeader.Split(' ');
			var authorizationType = bits[0];
			var isBasicAuth = authorizationType == "Basic";
			return isBasicAuth
				       ? CheckIsAuthenticated(context, bits[1])
				       : UnauthorizedResult.ForUnsupportedAuthorizationMethod();
		}

		private static IResult CheckIsAuthenticated(HttpContext context, string encodedCredentials)
		{
			var credentials = DecodeCredentials(encodedCredentials);
			var isAuthenticated = credentials == "username:password";
			return isAuthenticated
				       ? CheckIsOptions(context, context.Request.Path)
				       : UnauthorizedResult.ForInvalidCredentials();
		}

		private static string DecodeCredentials(string encodedCredentials)
		{
			var credentialBytes = Convert.FromBase64String(encodedCredentials);
			return Encoding.Unicode.GetString(credentialBytes);
		}

		private static IResult CheckIsOptions(HttpContext context, string path)
		{
			// TODO: Authorize against endpoint?

			return IsOptions(context)
				       ? DoOptions(path)
				       : CheckEndpointExists(context, path);
		}

		private static IResult CheckEndpointExists(HttpContext context, string path)
		{
			var endpoint = GetEndpoint(path);
			return endpoint == null
				       ? new NotFoundResult()
				       : CheckMethodIsSupported(context, endpoint);
		}

		private static IResult CheckMethodIsSupported(HttpContext context, Endpoint endpoint)
		{
			return endpoint.SupportsMethod(context)
				       ? Ok(endpoint)
				       : new MethodNotAllowedResult(endpoint);
		}

		private static IResult Ok(Endpoint endpoint)
		{
			return new OkResponse(endpoint);
		}

		private static bool IsOptions(HttpContext context)
		{
			return context.Request.HttpMethod == HttpMethod.Options.ToString();
		}

		private static OptionsResult DoOptions(string path)
		{
			var endpoint = GetEndpoint(path);
			return new OptionsResult(endpoint);
		}

		private static Endpoint GetEndpoint(string path)
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

	internal class OptionsResult : IResult
	{
		private readonly Endpoint _endpoint;

		public OptionsResult(Endpoint endpoint)
		{
			_endpoint = endpoint;
		}

		public void Update(HttpResponse response)
		{
			response.StatusCode = (int) HttpStatusCode.OK;
			if (_endpoint == null)
			{
				response.Headers.Add("Allow", " ");
			}
			else
			{
				response.StatusCode = (int) HttpStatusCode.OK;
				var allowHeader = _endpoint.GetAllowHeader();
				response.Headers.Add("Allow", allowHeader);
			}
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