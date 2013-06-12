using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	internal class AuthorizationHandler : IHandler
	{
		private readonly IHandler _successor;

		public AuthorizationHandler(IHandler optionsHandler)
		{
			_successor = optionsHandler;
		}

		public IResult Handle(RequestDetails requestDetails)
		{
			return requestDetails.IsAuthorized
				       ? _successor.Handle(requestDetails)
				       : UnauthorizedResult.ForInsufficientPrivileges();
		}
	}
}