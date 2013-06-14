using System.Net.Http;
using Nonae.Core.Endpoints;

namespace Nonae.Web
{
	public class HttpHandler:Core.HttpHandler
	{
		public HttpHandler() : base(new BasicAuthenticationProvider())
		{
			Add(Endpoint.AtUrl("/nonae").WithMethods(HttpMethod.Get, HttpMethod.Head, HttpMethod.Options));
			Add(Endpoint.AtUrl("/nonae/users").WithMethods(HttpMethod.Get, HttpMethod.Head, HttpMethod.Post, HttpMethod.Options));
			Add(Endpoint.AtUrl("/nonae/users/1").WithMethods(HttpMethod.Get, HttpMethod.Head, HttpMethod.Put, HttpMethod.Delete, HttpMethod.Options));
			Add(Endpoint.AtUrl("/nonae/secrets").WithMethods(HttpMethod.Get, HttpMethod.Head, HttpMethod.Put, HttpMethod.Options).AuthorizedWhen(user => user.Username == "admin"));
		}
	}
}