using System.Net.Http;
using Nonae.Core.Authorization;
using Nonae.Core.Endpoints;
using Nonae.Core.Requests;
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

		public IResult Handle(IRequestDetails requestDetails, IEndpointDetails endpoint, ICredentials credentials)
		{
			return requestDetails.Answers(HttpMethod.Put) 
				? _putHandler.Handle(requestDetails, endpoint, credentials) 
				: NotFoundResult.ForNonexistentResource();
		}
	}
}