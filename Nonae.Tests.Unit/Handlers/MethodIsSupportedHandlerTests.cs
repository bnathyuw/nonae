using NUnit.Framework;
using Nonae.Core.Handlers;
using Nonae.Core.Results;
using Rhino.Mocks;

namespace Nonae.Tests.Unit.Handlers
{
	[TestFixture]
	public class MethodIsSupportedHandlerTests
	{
		private IHandler _successor;
		private MethodIsSupportedHandler _handler;
		private IRequestDetails _requestDetails;

		[SetUp]
		public void SetUp()
		{
			_successor = MockRepository.GenerateStub<IHandler>();
			_handler = new MethodIsSupportedHandler(_successor);
			_requestDetails = MockRepository.GenerateStub<IRequestDetails>();
		}

		[Test]
		public void Returns_result_from_successor_if_the_method_is_supported()
		{
			_requestDetails.Stub(rd => rd.MethodIsSupported).Return(true);
			var expectedResult = MockRepository.GenerateStub<IResult>();
			_successor.Stub(s => s.Handle(_requestDetails)).Return(expectedResult);

			var result = _handler.Handle(_requestDetails);

			_successor.AssertWasCalled(s => s.Handle(_requestDetails));
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test]
		public void Returns_method_not_allows_if_the_method_is_not_supported()
		{
			_requestDetails.Stub(rd => rd.MethodIsSupported).Return(false);

			var result = _handler.Handle(_requestDetails);

			Assert.That(result, Is.TypeOf<MethodNotAllowedResult>());
		}
	}
}