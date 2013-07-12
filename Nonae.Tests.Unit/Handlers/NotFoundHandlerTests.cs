using System.Net.Http;
using NUnit.Framework;
using Nonae.Core.Endpoints;
using Nonae.Core.Handlers;
using Nonae.Core.Requests;
using Nonae.Core.Results;
using Rhino.Mocks;

namespace Nonae.Tests.Unit.Handlers
{
	[TestFixture]
	public class NotFoundHandlerTests
	{
	    private IEndpointDetails _endpointDetails;
	    private IRequestDetails _requestDetails;
	    private NotFoundHandler _notFoundHandler;

	    [Test]
		public void Returns_not_found_result()
		{
			_notFoundHandler = new NotFoundHandler(null);
            _requestDetails = MockRepository.GenerateStub<IRequestDetails>();
		    _endpointDetails = MockRepository.GenerateStub<IEndpointDetails>();
            
            var result = _notFoundHandler.Handle(_requestDetails, _endpointDetails);

			Assert.That(result, Is.TypeOf<NotFoundResult>());
		}

		[Test]
		public void Returns_result_from_put_handler()
		{
			var putHandler = MockRepository.GenerateStub<IHandler>();
			var requestDetails = MockRepository.GenerateStub<IRequestDetails>();
			var expectedResult = MockRepository.GenerateStub<IResult>();
			putHandler.Stub(ph => ph.Handle(requestDetails, _endpointDetails)).Return(expectedResult);
			var notFoundHandler = new NotFoundHandler(putHandler);
			requestDetails.Stub(rd => rd.Answers(HttpMethod.Put)).Return(true);

			var result = notFoundHandler.Handle(requestDetails, _endpointDetails);

			putHandler.AssertWasCalled(ph => ph.Handle(requestDetails, _endpointDetails));
			Assert.That(result, Is.EqualTo(expectedResult));
		}
	}
}