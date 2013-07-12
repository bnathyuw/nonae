using NUnit.Framework;
using Nonae.Core.Authorization;
using Nonae.Core.Endpoints;
using Nonae.Core.Handlers;
using Nonae.Core.Results;
using Rhino.Mocks;

namespace Nonae.Tests.Unit.Handlers
{
	public class ResourceExistsHandlerTests
	{
		private IHandler _resourceFoundSuccessor;
		private IHandler _resourceNotFoundSuccessor;
		private ResourceExistsHandler _handler;
	    private IEndpointDetails _endpointDetails;
	    private ICredentials _credentials;

	    [SetUp]
		public void SetUp()
		{
			_resourceFoundSuccessor = MockRepository.GenerateStub<IHandler>();
			_resourceNotFoundSuccessor = MockRepository.GenerateStub<IHandler>();
			_handler = new ResourceExistsHandler(_resourceFoundSuccessor, _resourceNotFoundSuccessor);
		    _endpointDetails = MockRepository.GenerateStub<IEndpointDetails>();
	        _credentials = MockRepository.GenerateStub<ICredentials>();
		}

		[Test]
		public void Returns_result_from_resource_exists_successor_if_the_resource_exists()
		{
			var expectedResult = MockRepository.GenerateStub<IResult>();
            _endpointDetails.Stub(rd => rd.ResourceExists).Return(true);
            _resourceFoundSuccessor.Stub(s => s.Handle(_endpointDetails, _credentials, null)).Return(expectedResult);

            var result = _handler.Handle(_endpointDetails, _credentials, null);

            _resourceFoundSuccessor.AssertWasCalled(s => s.Handle(_endpointDetails, _credentials, null));
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test]
		public void Returns_result_from_resource_not_found_successor_if_the_resource_does_not_exist()
		{
			var expectedResult = MockRepository.GenerateStub<IResult>();
            _endpointDetails.Stub(rd => rd.ResourceExists).Return(false);
            _resourceNotFoundSuccessor.Stub(s => s.Handle(_endpointDetails, _credentials, null)).Return(expectedResult);

            var result = _handler.Handle(_endpointDetails, _credentials, null);

            _resourceNotFoundSuccessor.AssertWasCalled(s => s.Handle(_endpointDetails, _credentials, null));
			Assert.That(result, Is.EqualTo(expectedResult));
		} 
	}

}