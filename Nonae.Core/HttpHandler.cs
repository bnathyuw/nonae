using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace Nonae.Core
{
	public class HttpHandler : IHttpHandler
	{
		private readonly Dictionary<string, IEnumerable<HttpMethod>> _allow = new Dictionary<string, IEnumerable<HttpMethod>>();

		public void ProcessRequest(HttpContext context)
		{
			var path = context.Request.Path;
	
			var httpMethods = GetAllowedMethods(path);
			
			if (!httpMethods.Contains(new HttpMethod(context.Request.HttpMethod)))
				context.Response.StatusCode = (int) HttpStatusCode.MethodNotAllowed;

			SetAllowHeader(context, httpMethods);
		}

		private List<HttpMethod> GetAllowedMethods(string path)
		{
			return _allow[path].ToList();
		}

		private static void SetAllowHeader(HttpContext context, IEnumerable<HttpMethod> httpMethods)
		{
			var allow = string.Join(", ", httpMethods);
			context.Response.Headers.Add("Allow", allow);
		}

		public bool IsReusable { get { return true; } }

		protected void AddAllowedMethods(string url, params HttpMethod[] methods)
		{
			_allow.Add(url, methods);
		}
	}
}
