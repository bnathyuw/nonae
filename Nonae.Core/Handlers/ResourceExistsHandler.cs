using Nonae.Core.Requests;
using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	public class ResourceExistsHandler:IHandler
	{
		private readonly IHandler _successor;
		private readonly IResourceRepository _resourceRepository;

		public ResourceExistsHandler(IHandler successor, IResourceRepository resourceRepository)
		{
			_successor = successor;
			_resourceRepository = resourceRepository;
		}

		public IResult Handle(IRequestDetails requestDetails)
		{
			return _resourceRepository.Exists(null) ? _successor.Handle(requestDetails) : new NotFoundResult("Resource Not Found");
		}
	}


	public interface IResourceRepository
	{
		bool Exists(dynamic query);
	}
}