using System.Net;
using NUnit.Framework;
using Nonae.Core.Endpoints;
using Nonae.Core.Responses;
using Nonae.Core.Results;
using Rhino.Mocks;

namespace Nonae.Tests.Unit.Results
{
	[TestFixture]
	public class OptionsResultTests
	{
		private const string AllowAllTheThings = "Allow all the things";
		private OptionsResult _result;
		private IResponseDetails _responseDetails;
	    private IEndpointDetails _endpointDetails;

	    [TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
		    _endpointDetails = MockRepository.GenerateStub<IEndpointDetails>();
		    _endpointDetails.Stub(ed => ed.AllowHeader).Return(AllowAllTheThings);
		    _result = new OptionsResult(_endpointDetails);
		    _responseDetails = MockRepository.GenerateStub<IResponseDetails>();

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
            _endpointDetails.AssertWasCalled(ed => ed.AllowHeader);
			_responseDetails.AssertWasCalled(rd => rd.Allow = AllowAllTheThings);
		}
	}
}