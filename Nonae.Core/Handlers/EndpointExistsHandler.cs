using System.Web;
using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	internal class EndpointExistsHandler
	{
		private readonly MethodIsSupportedHandler _methodIsSupportedHandler;

		public EndpointExistsHandler()
		{
			_methodIsSupportedHandler = new MethodIsSupportedHandler();
		}

		public IResult CheckEndpointExists(HttpContext context, string path)
		{
			var endpoint = EndpointStore.Get(path);
			return endpoint == null
				       ? new NotFoundResult()
				       : _methodIsSupportedHandler.CheckMethodIsSupported(context, endpoint);
		}
	}
}