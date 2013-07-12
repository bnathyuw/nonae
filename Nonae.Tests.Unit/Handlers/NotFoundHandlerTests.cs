using System.Net.Http;
using NUnit.Framework;
using Nonae.Core.Authorization;
using Nonae.Core.Endpoints;
using Nonae.Core.Handlers;
using Nonae.Core.Results;
using Rhino.Mocks;

namespace Nonae.Tests.Unit.Handlers
{
	[TestFixture]
	public class NotFoundHandlerTests
	{
	    private IEndpointDetails _endpointDetails;
	    private NotFoundHandler _notFoundHandler;
	    private ICredentials _credentials;

	    [Test]
		public void Returns_not_found_result()
		{
			_notFoundHandler = new NotFoundHandler(null);
		    _endpointDetails = MockRepository.GenerateStub<IEndpointDetails>();
	        _credentials = MockRepository.GenerateStub<ICredentials>();

            var result = _notFoundHandler.Handle(_endpointDetails, _credentials, null);

			Assert.That(result, Is.TypeOf<NotFoundResult>());
		}

		[Test]
		public void Returns_result_from_put_handler()
		{
			var putHandler = MockRepository.GenerateStub<IHandler>();
			var expectedResult = MockRepository.GenerateStub<IResult>();
            putHandler.Stub(ph => ph.Handle(_endpointDetails, _credentials, HttpMethod.Put)).Return(expectedResult);
			var notFoundHandler = new NotFoundHandler(putHandler);

            var result = notFoundHandler.Handle(_endpointDetails, _credentials, HttpMethod.Put);

            putHandler.AssertWasCalled(ph => ph.Handle(_endpointDetails, _credentials, HttpMethod.Put));
			Assert.That(result, Is.EqualTo(expectedResult));
		}
	}
}