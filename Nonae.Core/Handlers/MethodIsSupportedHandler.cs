using System.Web;
using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	internal class MethodIsSupportedHandler
	{
		private readonly OkHandler _okHandler;

		public MethodIsSupportedHandler()
		{
			_okHandler = new OkHandler();
		}

		public IResult CheckMethodIsSupported(HttpContext context, Endpoint endpoint)
		{
			return endpoint.SupportsMethod(context)
				       ? _okHandler.Ok(endpoint)
				       : new MethodNotAllowedResult(endpoint);
		}
	}
}