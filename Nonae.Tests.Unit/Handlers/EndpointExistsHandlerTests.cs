﻿using NUnit.Framework;
using Nonae.Core.Endpoints;
using Nonae.Core.Handlers;
using Nonae.Core.Requests;
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
	    private IEndpointDetails _endpointDetails;

	    [SetUp]
		public void SetUp()
		{
			_successor = MockRepository.GenerateStub<IHandler>();
			_handler = new EndpointExistsHandler(_successor);
			_requestDetails = MockRepository.GenerateStub<IRequestDetails>();
		    _endpointDetails = MockRepository.GenerateStub<IEndpointDetails>();
		}

		[Test]
		public void Returns_result_from_successor_if_the_endpoint_exists()
		{
            _endpointDetails.Stub(rd => rd.Exists).Return(true);
			var expectedResult = MockRepository.GenerateStub<IResult>();
            _successor.Stub(s => s.Handle(_requestDetails, _endpointDetails)).Return(expectedResult);

            var result = _handler.Handle(_requestDetails, _endpointDetails);

            _successor.AssertWasCalled(s => s.Handle(_requestDetails, _endpointDetails));
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test]
		public void Returns_not_found_if_then_endpoint_does_not_exist()
		{
            _endpointDetails.Stub(rd => rd.Exists).Return(false);

            var result = _handler.Handle(_requestDetails, _endpointDetails);

			Assert.That(result, Is.TypeOf<NotFoundResult>());
		}
	}
}