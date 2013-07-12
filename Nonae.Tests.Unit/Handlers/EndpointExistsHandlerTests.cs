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
	public class EndpointExistsHandlerTests
	{
		private IHandler _successor;
		private EndpointExistsHandler _handler;
	    private IEndpointDetails _endpointDetails;
	    private ICredentials _credentials;

	    [SetUp]
		public void SetUp()
		{
			_successor = MockRepository.GenerateStub<IHandler>();
			_handler = new EndpointExistsHandler(_successor);
		    _endpointDetails = MockRepository.GenerateStub<IEndpointDetails>();
	        _credentials = MockRepository.GenerateStub<ICredentials>();
		}

		[Test]
		public void Returns_result_from_successor_if_the_endpoint_exists()
		{
            _endpointDetails.Stub(rd => rd.Exists).Return(true);
			var expectedResult = MockRepository.GenerateStub<IResult>();
            _successor.Stub(s => s.Handle(_endpointDetails, _credentials, null)).Return(expectedResult);

            var result = _handler.Handle(_endpointDetails, _credentials, null);

            _successor.AssertWasCalled(s => s.Handle(_endpointDetails, _credentials, null));
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test]
		public void Returns_not_found_if_then_endpoint_does_not_exist()
		{
            _endpointDetails.Stub(rd => rd.Exists).Return(false);

            var result = _handler.Handle(_endpointDetails, _credentials, null);

			Assert.That(result, Is.TypeOf<NotFoundResult>());
		}
	}
}