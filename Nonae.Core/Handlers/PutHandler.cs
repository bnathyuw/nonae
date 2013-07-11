using Nonae.Core.Requests;
using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	public class PutHandler:IHandler
	{
		public IResult Handle(IRequestDetails requestDetails)
		{
			return new CreatedResult(requestDetails);
		}
	}
}