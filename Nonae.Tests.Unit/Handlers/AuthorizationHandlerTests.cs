using NUnit.Framework;
using Nonae.Core.Authorization;
using Nonae.Core.Endpoints;
using Nonae.Core.Handlers;
using Nonae.Core.Requests;
using Nonae.Core.Results;
using Rhino.Mocks;

namespace Nonae.Tests.Unit.Handlers
{
	[TestFixture]
	public class AuthorizationHandlerTests
	{
		private IHandler _successor;
		private AuthorizationHandler _handler;
		private IRequestDetails _requestDetails;
	    private IEndpointDetails _endpointDetails;
	    private ICredentials _credentials;

	    [SetUp]
		public void SetUp()
		{
			_successor = MockRepository.GenerateStub<IHandler>();
			_handler = new AuthorizationHandler(_successor);
			_requestDetails = MockRepository.GenerateStub<IRequestDetails>();
		    _endpointDetails = MockRepository.GenerateStub<IEndpointDetails>();
	        _credentials = MockRepository.GenerateStub<ICredentials>();
		}

		[Test]
		public void Returns_result_from_successor_if_the_request_is_authorized()
		{
			_requestDetails.Stub(rd => rd.GetIsAuthorized(_endpointDetails)).Return(true);
			var expectedResult = MockRepository.GenerateStub<IResult>();
            _successor.Stub(s => s.Handle(_requestDetails, _endpointDetails, _credentials)).Return(expectedResult);

            var result = _handler.Handle(_requestDetails, _endpointDetails, _credentials);

            _successor.AssertWasCalled(s => s.Handle(_requestDetails, _endpointDetails, _credentials));
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test]
		public void Returns_unauthorized_if_the_request_is_not_authorized()
		{
			_requestDetails.Stub(rd => rd.GetIsAuthorized(_endpointDetails)).Return(false);

            var result = _handler.Handle(_requestDetails, _endpointDetails, _credentials);

			Assert.That(result, Is.TypeOf<UnauthorizedResult>());
		}
	}
}