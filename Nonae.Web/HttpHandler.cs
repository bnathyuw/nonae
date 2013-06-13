using System.Net.Http;
using Nonae.Core.Authorization;

namespace Nonae.Web
{
	public class HttpHandler:Core.HttpHandler
	{
		public HttpHandler() : base(new BasicAuthenticationProvider())
		{
			AddEndpoint("/nonae").WithMethods(HttpMethod.Get, HttpMethod.Head, HttpMethod.Options);
			AddEndpoint("/nonae/users").WithMethods(HttpMethod.Get, HttpMethod.Head, HttpMethod.Post, HttpMethod.Options);
			AddEndpoint("/nonae/users/1").WithMethods(HttpMethod.Get, HttpMethod.Head, HttpMethod.Put, HttpMethod.Delete, HttpMethod.Options);
			AddEndpoint("/nonae/secrets").WithMethods(HttpMethod.Get, HttpMethod.Head, HttpMethod.Put, HttpMethod.Options).AuthorizedWhen(user => user.Username == "admin");
		}
	}
}