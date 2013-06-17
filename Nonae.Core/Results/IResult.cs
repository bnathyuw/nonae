using Nonae.Core.Responses;

namespace Nonae.Core.Results
{
	public interface IResult
	{
		void Update(IResponseDetails responseDetails);
	}
}