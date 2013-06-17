using System.Net;
using NUnit.Framework;
using Nonae.Core.Requests;
using Nonae.Core.Responses;
using Nonae.Core.Results;
using Rhino.Mocks;

namespace Nonae.Tests.Unit.Results
{
	[TestFixture]
	public class OkResultTests
	{
		private const string AllowAllTheThings = "Allow all the things";
		private OkResult _result;
		private IResponseDetails _responseDetails;
		private IRequestDetails _requestDetails;

		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			_requestDetails = MockRepository.GenerateStub<IRequestDetails>();
			_result = new OkResult(_requestDetails);
			_responseDetails = MockRepository.GenerateStub<IResponseDetails>();
			_requestDetails.Stub(rd => rd.AllowHeader).Return(AllowAllTheThings);

			_result.Update(_responseDetails);
		}

		[Test]
		public void Sets_status_code_to_ok()
		{
			_responseDetails.AssertWasCalled(rd => rd.StatusCode = HttpStatusCode.OK);
		}

		[Test]
		public void Sets_allow_header_to_value_from_request_details()
		{
			_requestDetails.AssertWasCalled(rd => rd.AllowHeader);
			_responseDetails.AssertWasCalled(rd => rd.Allow = AllowAllTheThings);
		}
	}
}
