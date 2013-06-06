using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	internal interface IHandler
	{
		IResult Handle(RequestDetails requestDetails);
	}
}