using System.Net;
using NUnit.Framework;
using Nonae.Core.Requests;
using Nonae.Core.Responses;
using Nonae.Core.Results;
using Rhino.Mocks;

namespace Nonae.Tests.Unit.Results
{
	[TestFixture]
	public class MethodNotAllowedResultTests
	{
		private IRequestDetails _requestDetails;
		private MethodNotAllowedResult _result;
		private IResponseDetails _responseDetails;

		[SetUp]
		public void SetUp()
		{
			_requestDetails = MockRepository.GenerateStub<IRequestDetails>();
			_result = new MethodNotAllowedResult(_requestDetails);
			_responseDetails = MockRepository.GenerateStub<IResponseDetails>();
		}

		[Test]
		public void Sets_status_code_to_method_not_allowed()
		{
			_result.Update(_responseDetails);

			_responseDetails.AssertWasCalled(rd => rd.StatusCode = HttpStatusCode.MethodNotAllowed);
		}

		[Test]
		public void Sets_allow_header_to_value_from_request_details()
		{
			const string allowHeader = "Allow all the things";
			_requestDetails.Stub(rd => rd.AllowHeader).Return(allowHeader);

			_result.Update(_responseDetails);

			_requestDetails.AssertWasCalled(rd => rd.AllowHeader);
			_responseDetails.AssertWasCalled(rd => rd.Allow = allowHeader);
		}
	}
}