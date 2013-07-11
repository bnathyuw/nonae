using Nonae.Core.Requests;
using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	internal class NotFoundHandler : IHandler
	{
		public IResult Handle(IRequestDetails requestDetails)
		{
			return requestDetails.IsPutRequest ? (IResult) new OkResult(requestDetails) : new NotFoundResult("Resource Not Found");
		}
	}
}