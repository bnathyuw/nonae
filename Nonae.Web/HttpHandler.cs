using System.Net.Http;

namespace Nonae.Web
{
	public class HttpHandler:Core.HttpHandler
	{
		public HttpHandler()
		{
			AddEndpoint("/nonae").WithMethods(HttpMethod.Get, HttpMethod.Head, HttpMethod.Options);
			AddEndpoint("/nonae/users").WithMethods(HttpMethod.Get, HttpMethod.Head, HttpMethod.Post, HttpMethod.Options);
			AddEndpoint("/nonae/users/1").WithMethods(HttpMethod.Get, HttpMethod.Head, HttpMethod.Put, HttpMethod.Delete, HttpMethod.Options);
			AddEndpoint("/nonae/secrets").WithMethods(HttpMethod.Get, HttpMethod.Head, HttpMethod.Put, HttpMethod.Options).Authorize(user => user.Username == "admin");
		}
	}
}