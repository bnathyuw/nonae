using NUnit.Framework;
using Nonae.Core.Handlers;
using Nonae.Core.Requests;
using Nonae.Core.Results;
using Rhino.Mocks;

namespace Nonae.Tests.Unit.Handlers
{
	public class ResourceExistsHandlerTests
	{
		private IHandler _successor;
		private ResourceExistsHandler _handler;
		private IRequestDetails _requestDetails;
		private IResourceRepository _resourceRepository;

		[SetUp]
		public void SetUp()
		{
			_successor = MockRepository.GenerateStub<IHandler>();
			_resourceRepository = MockRepository.GenerateStub<IResourceRepository>();
			_handler = new ResourceExistsHandler(_successor, _resourceRepository);
			_requestDetails = MockRepository.GenerateStub<IRequestDetails>();
		}

		[Test]
		public void Returns_result_from_successor_if_the_resource_exists()
		{
			var expectedResult = MockRepository.GenerateStub<IResult>();
			_successor.Stub(s => s.Handle(_requestDetails)).Return(expectedResult);
			_resourceRepository.Stub(rr => rr.Exists(Arg<dynamic>.Is.Anything)).Return(true);

			var result = _handler.Handle(_requestDetails);

			_resourceRepository.AssertWasCalled(rr => rr.Exists(Arg<dynamic>.Is.Anything));
			_successor.AssertWasCalled(s => s.Handle(_requestDetails));
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test]
		public void Returns_not_found_if_the_resource_does_not_exist()
		{
			_resourceRepository.Stub(rr => rr.Exists(Arg<dynamic>.Is.Anything)).Return(false);

			var result = _handler.Handle(_requestDetails);

			Assert.That(result, Is.TypeOf<NotFoundResult>());
		} 
	}

}