using System.Net.Http;
using System.Web;
using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	internal class OptionsHandler
	{
		private readonly EndpointExistsHandler _endpointExistsHandler;

		public OptionsHandler()
		{
			_endpointExistsHandler = new EndpointExistsHandler();
		}

		public IResult CheckIsOptions(HttpContext context, string path)
		{
			// TODO: Authorize against endpoint?

			return IsOptions(context)
				       ? new OptionsResult(HttpHandler.GetEndpoint(path))
				       : _endpointExistsHandler.CheckEndpointExists(context, path);
		}

		private static bool IsOptions(HttpContext context)
		{
			return context.Request.HttpMethod == HttpMethod.Options.ToString();
		}
	}
}