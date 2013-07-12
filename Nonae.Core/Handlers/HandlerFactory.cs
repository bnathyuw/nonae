namespace Nonae.Core.Handlers
{
    static internal class HandlerFactory
    {
        public static IHandler Build()
        {
            var okHandler = new OkHandler();
            var putHandler = new PutHandler();
            var notFoundHandler = new NotFoundHandler(putHandler);
            var methodIsSupportedHandler = new MethodIsSupportedHandler(okHandler);
            var resourceExistsHandler = new ResourceExistsHandler(methodIsSupportedHandler, notFoundHandler);
            var endpointExistsHandler = new EndpointExistsHandler(resourceExistsHandler);
            var optionsHandler = new OptionsHandler(endpointExistsHandler);
            var authorizationHandler = new AuthorizationHandler(optionsHandler);
            var authenticationHandler = new AuthenticationHandler(authorizationHandler);
            return authenticationHandler;
        }
    }
}