using System.Web;
using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	internal class AuthenticationHandler
	{
		private readonly OptionsHandler _optionsHandler;

		public AuthenticationHandler()
		{
			_optionsHandler = new OptionsHandler();
		}

		public IResult CheckAuthentication(HttpContext context)
		{
			var authorizationHeader = context.Request.Headers["Authorization"];

			return authorizationHeader == null 
				? _optionsHandler.CheckIsOptions(context, context.Request.Path) 
				: CheckAuthenticationFromHeader(context, authorizationHeader);
		}

		private IResult CheckAuthenticationFromHeader(HttpContext context, string authorizationHeader)
		{
			var authorizationDetails = AuthorizationDetails.From(authorizationHeader);

			return authorizationDetails == null
				       ? UnauthorizedResult.ForUnsupportedAuthorizationMethod()
				       : CheckBasicAuth(authorizationDetails, context);
		}

		private IResult CheckBasicAuth(AuthorizationDetails authorizationDetails, HttpContext context)
		{
			return authorizationDetails.IsAuthenticated
				       ? _optionsHandler.CheckIsOptions(context, context.Request.Path)
				       : UnauthorizedResult.ForInvalidCredentials();
		}
	}
}
