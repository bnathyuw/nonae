using NUnit.Framework;
using Nonae.Core.Endpoints;
using Nonae.Core.Handlers;
using Nonae.Core.Requests;
using Nonae.Core.Results;
using Rhino.Mocks;

namespace Nonae.Tests.Unit.Handlers
{
	public class ResourceExistsHandlerTests
	{
		private IHandler _resourceFoundSuccessor;
		private IHandler _resourceNotFoundSuccessor;
		private ResourceExistsHandler _handler;
		private IRequestDetails _requestDetails;
	    private IEndpointDetails _endpointDetails;

	    [SetUp]
		public void SetUp()
		{
			_resourceFoundSuccessor = MockRepository.GenerateStub<IHandler>();
			_resourceNotFoundSuccessor = MockRepository.GenerateStub<IHandler>();
			_handler = new ResourceExistsHandler(_resourceFoundSuccessor, _resourceNotFoundSuccessor);
			_requestDetails = MockRepository.GenerateStub<IRequestDetails>();
		    _endpointDetails = MockRepository.GenerateStub<IEndpointDetails>();
		}

		[Test]
		public void Returns_result_from_resource_exists_successor_if_the_resource_exists()
		{
			var expectedResult = MockRepository.GenerateStub<IResult>();
			_requestDetails.Stub(rd => rd.ResourceExists).Return(true);
			_resourceFoundSuccessor.Stub(s => s.Handle(_requestDetails, _endpointDetails)).Return(expectedResult);

			var result = _handler.Handle(_requestDetails, _endpointDetails);

			_resourceFoundSuccessor.AssertWasCalled(s => s.Handle(_requestDetails, _endpointDetails));
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test]
		public void Returns_result_from_resource_not_found_successor_if_the_resource_does_not_exist()
		{
			var expectedResult = MockRepository.GenerateStub<IResult>();
			_requestDetails.Stub(rd => rd.ResourceExists).Return(false);
			_resourceNotFoundSuccessor.Stub(s => s.Handle(_requestDetails, _endpointDetails)).Return(expectedResult);

			var result = _handler.Handle(_requestDetails, _endpointDetails);

			_resourceNotFoundSuccessor.AssertWasCalled(s => s.Handle(_requestDetails, _endpointDetails));
			Assert.That(result, Is.EqualTo(expectedResult));
		} 
	}

}