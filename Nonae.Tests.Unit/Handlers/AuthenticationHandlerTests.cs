using NUnit.Framework;
using Nonae.Core.Endpoints;
using Nonae.Core.Handlers;
using Nonae.Core.Requests;
using Nonae.Core.Results;
using Rhino.Mocks;

namespace Nonae.Tests.Unit.Handlers
{
	[TestFixture]
	public class AuthenticationHandlerTests
	{
		private IHandler _successor;
		private AuthenticationHandler _handler;
		private IRequestDetails _requestDetails;
	    private IEndpointDetails _endpointDetails;

	    [SetUp]
		public void SetUp()
		{
			_successor = MockRepository.GenerateStub<IHandler>();
			_handler = new AuthenticationHandler(_successor);
			_requestDetails = MockRepository.GenerateStub<IRequestDetails>();
	        _endpointDetails = MockRepository.GenerateStub<IEndpointDetails>();
		}

		[Test]
		public void Returns_result_from_successor_if_the_request_has_no_authorization()
		{
			_requestDetails.Stub(rd => rd.HasAuthorization).Return(false);
			var expectedResult = MockRepository.GenerateStub<IResult>();
			_successor.Stub(s => s.Handle(_requestDetails, _endpointDetails)).Return(expectedResult);

			var result = _handler.Handle(_requestDetails, _endpointDetails);

			_successor.AssertWasCalled(s => s.Handle(_requestDetails, _endpointDetails));
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test]
		public void Returns_unauthorized_if_the_request_is_not_authenticated()
		{
			_requestDetails.Stub(rd => rd.HasAuthorization).Return(true);
			_requestDetails.Stub(rd => rd.IsAuthenticated).Return(false);

			var handle = _handler.Handle(_requestDetails, _endpointDetails);

			Assert.That(handle, Is.TypeOf<UnauthorizedResult>());
		}

		[Test]
		public void Returns_result_from_successor_if_the_request_is_authenticated()
		{
			_requestDetails.Stub(rd => rd.HasAuthorization).Return(true);
			_requestDetails.Stub(rd => rd.IsAuthenticated).Return(true);
			var expectedResult = MockRepository.GenerateStub<IResult>();
			_successor.Stub(s => s.Handle(_requestDetails, _endpointDetails)).Return(expectedResult);

			var result = _handler.Handle(_requestDetails, _endpointDetails);

			_successor.AssertWasCalled(s => s.Handle(_requestDetails, _endpointDetails));
			Assert.That(result, Is.EqualTo(expectedResult));
		}
	}
}