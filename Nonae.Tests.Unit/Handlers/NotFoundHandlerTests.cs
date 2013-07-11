using System.Net.Http;
using NUnit.Framework;
using Nonae.Core.Handlers;
using Nonae.Core.Requests;
using Nonae.Core.Results;
using Rhino.Mocks;

namespace Nonae.Tests.Unit.Handlers
{
	[TestFixture]
	public class NotFoundHandlerTests
	{
		[Test]
		public void Returns_not_found_result()
		{
			var notFoundHandler = new NotFoundHandler(null);

			var requestDetails = MockRepository.GenerateStub<IRequestDetails>();
			var result = notFoundHandler.Handle(requestDetails);

			Assert.That(result, Is.TypeOf<NotFoundResult>());
		}

		[Test]
		public void Returns_result_from_put_handler()
		{
			var putHandler = MockRepository.GenerateStub<IHandler>();
			var requestDetails = MockRepository.GenerateStub<IRequestDetails>();
			var expectedResult = MockRepository.GenerateStub<IResult>();
			putHandler.Stub(ph => ph.Handle(requestDetails)).Return(expectedResult);
			var notFoundHandler = new NotFoundHandler(putHandler);
			requestDetails.Stub(rd => rd.Answers(HttpMethod.Put)).Return(true);

			var result = notFoundHandler.Handle(requestDetails);

			putHandler.AssertWasCalled(ph => ph.Handle(requestDetails));
			Assert.That(result, Is.EqualTo(expectedResult));
		}
	}
}