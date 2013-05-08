using System.Net.Http;

namespace Nonae.Web
{
	public class HttpHandler:Core.HttpHandler
	{
		public HttpHandler()
		{
			AddAllowedMethods("/nonae", HttpMethod.Get, HttpMethod.Head, HttpMethod.Options);
			AddAllowedMethods("/nonae/users", HttpMethod.Get, HttpMethod.Head, HttpMethod.Post, HttpMethod.Options);
			AddAllowedMethods("/nonae/users/1", HttpMethod.Get, HttpMethod.Head, HttpMethod.Put, HttpMethod.Delete, HttpMethod.Options);
		}
	}
}