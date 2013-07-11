using System.Net.Http;
using Nonae.Core.Requests;
using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	internal class NotFoundHandler : IHandler
	{
		public IResult Handle(IRequestDetails requestDetails)
		{
			return requestDetails.Answers(HttpMethod.Put) ? (IResult) new OkResult(requestDetails) : NotFoundResult.ForNonexistentResource();
		}
	}
}