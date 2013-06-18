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
		private const string Message = "Fa la la";

		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			_result = new NotFoundResult(Message);
			_responseDetails = MockRepository.GenerateStub<IResponseDetails>();
			_result.Update(_responseDetails);
		}

		[Test]
		public void Sets_status_code_to_not_found()
		{
			_responseDetails.AssertWasCalled(rd => rd.StatusCode = HttpStatusCode.NotFound);
		}


		[Test]
		public void Sets_body_to_message_supplied()
		{
			_responseDetails.AssertWasCalled(rd => rd.Body = Message);
		}
	}
}