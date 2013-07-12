using NUnit.Framework;
using Nonae.Core.Authorization;
using Nonae.Core.Endpoints;
using Nonae.Core.Handlers;
using Nonae.Core.Results;
using Rhino.Mocks;

namespace Nonae.Tests.Unit.Handlers
{
	[TestFixture]
	public class AuthenticationHandlerTests
	{
		private IHandler _successor;
		private AuthenticationHandler _handler;
	    private IEndpointDetails _endpointDetails;
	    private ICredentials _credentials;

	    [SetUp]
		public void SetUp()
		{
			_successor = MockRepository.GenerateStub<IHandler>();
			_handler = new AuthenticationHandler(_successor);
	        _endpointDetails = MockRepository.GenerateStub<IEndpointDetails>();
	        _credentials = MockRepository.GenerateStub<ICredentials>();
		}

		[Test]
		public void Returns_result_from_successor_if_the_request_has_no_authorization()
		{
            _credentials.Stub(c => c.IsAnonymous).Return(true);
			var expectedResult = MockRepository.GenerateStub<IResult>();
            _successor.Stub(s => s.Handle(_endpointDetails, _credentials, null)).Return(expectedResult);

            var result = _handler.Handle(_endpointDetails, _credentials, null);

            _successor.AssertWasCalled(s => s.Handle(_endpointDetails, _credentials, null));
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test]
		public void Returns_unauthorized_if_the_request_is_not_authenticated()
		{
            _credentials.Stub(c => c.IsAnonymous).Return(false);
            _credentials.Stub(c => c.IsAuthenticated).Return(false);

            var handle = _handler.Handle(_endpointDetails, _credentials, null);

			Assert.That(handle, Is.TypeOf<UnauthorizedResult>());
		}

		[Test]
		public void Returns_result_from_successor_if_the_request_is_authenticated()
		{
            _credentials.Stub(c => c.IsAnonymous).Return(false);
            _credentials.Stub(c => c.IsAuthenticated).Return(true);
			var expectedResult = MockRepository.GenerateStub<IResult>();
            _successor.Stub(s => s.Handle(_endpointDetails, _credentials, null)).Return(expectedResult);

            var result = _handler.Handle(_endpointDetails, _credentials, null);

            _successor.AssertWasCalled(s => s.Handle(_endpointDetails, _credentials, null));
			Assert.That(result, Is.EqualTo(expectedResult));
		}
	}
}