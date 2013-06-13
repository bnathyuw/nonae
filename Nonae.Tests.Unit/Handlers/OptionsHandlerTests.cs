using NUnit.Framework;
using Nonae.Core.Handlers;
using Nonae.Core.Requests;
using Nonae.Core.Results;
using Rhino.Mocks;

namespace Nonae.Tests.Unit.Handlers
{
	[TestFixture]
	public class OptionsHandlerTests
	{
		private IHandler _successor;
		private OptionsHandler _handler;
		private IRequestDetails _requestDetails;

		[SetUp]
		public void SetUp()
		{
			_successor = MockRepository.GenerateStub<IHandler>();
			_handler = new OptionsHandler(_successor);
			_requestDetails = MockRepository.GenerateStub<IRequestDetails>();
		}

		[Test]
		public void Returns_options_result_if_the_request_method_is_options()
		{
			_requestDetails.Stub(rd => rd.IsOptionsRequest).Return(true);

			var result = _handler.Handle(_requestDetails);

			Assert.That(result, Is.TypeOf<OptionsResult>());
		}

		[Test]
		public void Returns_result_from_successor_if_the_request_method_is_not_options()
		{
			_requestDetails.Stub(rd => rd.IsOptionsRequest).Return(false);
			var expectedResult = MockRepository.GenerateStub<IResult>();
			_successor.Stub(s => s.Handle(_requestDetails)).Return(expectedResult);

			var result = _handler.Handle(_requestDetails);

			_successor.AssertWasCalled(s => s.Handle(_requestDetails));
			Assert.That(result, Is.EqualTo(expectedResult));
		}
	}
}