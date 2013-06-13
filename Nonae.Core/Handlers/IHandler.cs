using Nonae.Core.Requests;
using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	public interface IHandler
	{
		IResult Handle(IRequestDetails requestDetails);
	}
}