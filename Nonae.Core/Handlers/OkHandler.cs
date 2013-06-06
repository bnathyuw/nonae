using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	internal class OkHandler : IHandler
	{
		public IResult Handle(RequestDetails requestDetails)
		{
			return new OkResponse(requestDetails.Endpoint);
		}
	}
}