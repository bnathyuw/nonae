using NUnit.Framework;
using Nonae.Core.Handlers;
using Nonae.Core.Results;
using Rhino.Mocks;

namespace Nonae.Tests.Unit.Handlers
{
	[TestFixture]
	public class EndpointExistsHandlerTests
	{
		private IHandler _successor;
		private EndpointExistsHandler _handler;
		private IRequestDetails _requestDetails;

		[SetUp]
		public void SetUp()
		{
			_successor = MockRepository.GenerateStub<IHandler>();
			_handler = new EndpointExistsHandler(_successor);
			_requestDetails = MockRepository.GenerateStub<IRequestDetails>();
		}

		[Test]
		public void Returns_result_from_successor_if_the_endpoint_exists()
		{
			_requestDetails.Stub(rd => rd.EndpointExists).Return(true);
			var expectedResult = MockRepository.GenerateStub<IResult>();
			_successor.Stub(s => s.Handle(_requestDetails)).Return(expectedResult);

			var result = _handler.Handle(_requestDetails);

			_successor.AssertWasCalled(s => s.Handle(_requestDetails));
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test]
		public void Returns_not_found_if_then_endpoint_does_not_exist()
		{
			_requestDetails.Stub(rd => rd.EndpointExists).Return(false);

			var result = _handler.Handle(_requestDetails);

			Assert.That(result, Is.TypeOf<NotFoundResult>());
		}
	}
}