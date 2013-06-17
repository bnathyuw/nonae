using System.Net;
using NUnit.Framework;
using Nonae.Core.Responses;
using Nonae.Core.Results;
using Rhino.Mocks;

namespace Nonae.Tests.Unit.Results
{
	[TestFixture]
	public class NotFoundResultTests
	{	
		private NotFoundResult _result;
		private IResponseDetails _responseDetails;

		[SetUp]
		public void SetUp()
		{
			_result = new NotFoundResult();
			_responseDetails = MockRepository.GenerateStub<IResponseDetails>();
		}

		[Test]
		public void Sets_status_code_to_not_found()
		{
			_result.Update(_responseDetails);

			_responseDetails.AssertWasCalled(rd => rd.StatusCode = HttpStatusCode.NotFound);
		}
	}
}