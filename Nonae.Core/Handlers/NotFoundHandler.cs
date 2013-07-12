using System.Net.Http;
using Nonae.Core.Authorization;
using Nonae.Core.Endpoints;
using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	internal class NotFoundHandler : IHandler
	{
		private readonly IHandler _putHandler;

		public NotFoundHandler(IHandler putHandler)
		{
			_putHandler = putHandler;
		}

		public IResult Handle(IEndpointDetails endpoint, ICredentials credentials, HttpMethod httpMethod)
		{
			return httpMethod == HttpMethod.Put 
				? _putHandler.Handle(endpoint, credentials, httpMethod) 
				: NotFoundResult.ForNonexistentResource();
		}
	}
}