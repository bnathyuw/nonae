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
			var notFoundHandler = new NotFoundHandler();

			var requestDetails = MockRepository.GenerateStub<IRequestDetails>();
			var result = notFoundHandler.Handle(requestDetails);

			Assert.That(result, Is.TypeOf<NotFoundResult>());
		}

		[Test]
		public void Returns_ok_for_put()
		{
			var notFoundHandler = new NotFoundHandler();

			var requestDetails = MockRepository.GenerateStub<IRequestDetails>();
			requestDetails.Stub(rd => rd.Answers(HttpMethod.Put)).Return(true);
			var result = notFoundHandler.Handle(requestDetails);

			Assert.That(result, Is.TypeOf<OkResult>());
		}
	}
}