namespace Nonae.Web
{
	public class HttpHandler:Core.HttpHandler
	{
		public HttpHandler()
		{
			_allow.Add("/nonae", "GET, HEAD");
			_allow.Add("/nonae/users", "POST, GET, HEAD");
			_allow.Add("/nonae/users/1", "GET, PUT, DELETE, HEAD");
		}
	}
}