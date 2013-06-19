using Nonae.Core.Requests;
using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	public class ResourceExistsHandler:IHandler
	{
		private readonly IHandler _successor;

		public ResourceExistsHandler(IHandler successor)
		{
			_successor = successor;
		}

		public IResult Handle(IRequestDetails requestDetails)
		{
			return requestDetails.ResourceExists
				? _successor.Handle(requestDetails) 
				: new NotFoundResult("Resource Not Found");
		}
	}
}