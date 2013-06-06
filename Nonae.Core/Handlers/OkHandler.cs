using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	internal class OkHandler
	{
		public IResult Ok(Endpoint endpoint)
		{
			return new OkResponse(endpoint);
		}
	}
}