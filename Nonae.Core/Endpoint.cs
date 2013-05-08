using System;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Nonae.Core
{
	public class Endpoint
	{
		private HttpMethod[] _methods;

		public bool SupportsMethod(HttpContext context)
		{
			return _methods.ToList().Contains(new HttpMethod(context.Request.HttpMethod));
		}

		public string GetAllowHeader()
		{
			return String.Join(", ", _methods.ToList());
		}

		public void WithMethods(params HttpMethod[] methods)
		{
			_methods = methods;
		}
	}
}