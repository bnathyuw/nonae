using NUnit.Framework;
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

	    [SetUp]
		public void SetUp()
		{
			_successor = MockRepository.GenerateStub<IHandler>();
			_handler = new AuthorizationHandler(_successor);
			_requestDetails = MockRepository.GenerateStub<IRequestDetails>();
		    _endpointDetails = MockRepository.GenerateStub<IEndpointDetails>();
		}

		[Test]
		public void Returns_result_from_successor_if_the_request_is_authorized()
		{
			_requestDetails.Stub(rd => rd.IsAuthorized).Return(true);
			var expectedResult = MockRepository.GenerateStub<IResult>();
            _successor.Stub(s => s.Handle(_requestDetails, _endpointDetails)).Return(expectedResult);

            var result = _handler.Handle(_requestDetails, _endpointDetails);

            _successor.AssertWasCalled(s => s.Handle(_requestDetails, _endpointDetails));
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test]
		public void Returns_unauthorized_if_the_request_is_not_authorized()
		{
			_requestDetails.Stub(rd => rd.IsAuthorized).Return(false);

            var result = _handler.Handle(_requestDetails, _endpointDetails);

			Assert.That(result, Is.TypeOf<UnauthorizedResult>());
		}
	}
}