using System.Net;
using NUnit.Framework;
using Nonae.Core.Responses;
using Nonae.Core.Results;
using Rhino.Mocks;

namespace Nonae.Tests.Unit.Results
{
	[TestFixture]
	public class UnauthorizedResultTests
	{
		private const string Message = "EAT ME!";
		private UnauthorizedResult _result;
		private IResponseDetails _responseDetails;

		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			_result = new UnauthorizedResult(Message);
			_responseDetails = MockRepository.GenerateStub<IResponseDetails>();
			_result.Update(_responseDetails);
		}

		[Test]
		public void Sets_status_code_to_unauthorized()
		{
			_responseDetails.AssertWasCalled(rd => rd.StatusCode = HttpStatusCode.Unauthorized);
		}

		[Test]
		public void Sets_www_authentication_to_basic()
		{
			_responseDetails.AssertWasCalled(rd => rd.WwwAuthenticate = "Basic realm=\"foo\"");
		}

		[Test]
		public void Sets_body_to_message_supplied()
		{
			_responseDetails.AssertWasCalled(rd => rd.Body = Message);
		}
	}
}