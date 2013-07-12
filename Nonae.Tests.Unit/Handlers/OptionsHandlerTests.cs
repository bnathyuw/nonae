using System.Net.Http;
using NUnit.Framework;
using Nonae.Core.Authorization;
using Nonae.Core.Endpoints;
using Nonae.Core.Handlers;
using Nonae.Core.Results;
using Rhino.Mocks;

namespace Nonae.Tests.Unit.Handlers
{
	[TestFixture]
	public class OptionsHandlerTests
	{
		private IHandler _successor;
		private OptionsHandler _handler;
	    private IEndpointDetails _endpointDetails;
	    private ICredentials _credentials;

	    [SetUp]
		public void SetUp()
		{
			_successor = MockRepository.GenerateStub<IHandler>();
			_handler = new OptionsHandler(_successor);
		    _endpointDetails = MockRepository.GenerateStub<IEndpointDetails>();
	        _credentials = MockRepository.GenerateStub<ICredentials>();
		}

		[Test]
		public void Returns_options_result_if_the_request_method_is_options()
		{
            var result = _handler.Handle(_endpointDetails, _credentials, HttpMethod.Options);

			Assert.That(result, Is.TypeOf<OptionsResult>());
		}

		[Test]
		public void Returns_result_from_successor_if_the_request_method_is_not_options()
		{
			var expectedResult = MockRepository.GenerateStub<IResult>();
            _successor.Stub(s => s.Handle(_endpointDetails, _credentials, HttpMethod.Get)).Return(expectedResult);

            var result = _handler.Handle(_endpointDetails, _credentials, HttpMethod.Get);

            _successor.AssertWasCalled(s => s.Handle(_endpointDetails, _credentials, HttpMethod.Get));
			Assert.That(result, Is.EqualTo(expectedResult));
		}
	}
}