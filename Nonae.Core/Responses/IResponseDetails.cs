using System.Net;

namespace Nonae.Core.Responses
{
	public interface IResponseDetails
	{
		HttpStatusCode StatusCode { set; }
		string WwwAuthenticate { set; }
		string Body { set; }
		string Allow { set; }
	}
}