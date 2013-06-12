using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	internal class OkHandler : IHandler
	{
		public IResult Handle(IRequestDetails requestDetails)
		{
			return new OkResult(requestDetails);
		}
	}
}