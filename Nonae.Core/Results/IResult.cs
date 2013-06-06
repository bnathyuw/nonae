using System.Web;

namespace Nonae.Core.Results
{
	public interface IResult
	{
		void Update(HttpResponse response);
	}
}