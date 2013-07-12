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
	public class MethodIsSupportedHandlerTests
	{
		private IHandler _successor;
		private MethodIsSupportedHandler _handler;
		private IEndpointDetails _endpointDetails;
		private ICredentials _credentials;

		[SetUp]
		public void SetUp()
		{
			_successor = MockRepository.GenerateStub<IHandler>();
			_handler = new MethodIsSupportedHandler(_successor);
			_endpointDetails = MockRepository.GenerateStub<IEndpointDetails>();
			_credentials = MockRepository.GenerateStub<ICredentials>();
		}

		[Test]
		public void Returns_result_from_successor_if_the_method_is_supported()
		{
			var httpMethod = HttpMethod.Delete;
			_endpointDetails.Stub(ed => ed.Allows(httpMethod)).Return(true);
			var expectedResult = MockRepository.GenerateStub<IResult>();
			_successor.Stub(s => s.Handle(_endpointDetails, _credentials, httpMethod)).Return(expectedResult);

			var result = _handler.Handle(_endpointDetails, _credentials, httpMethod);

			_successor.AssertWasCalled(s => s.Handle(_endpointDetails, _credentials, httpMethod));
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test]
		public void Returns_method_not_allows_if_the_method_is_not_supported()
		{
			var httpMethod = HttpMethod.Delete;
			_endpointDetails.Stub(ed => ed.Allows(httpMethod)).Return(false);

			var result = _handler.Handle(_endpointDetails, _credentials, httpMethod);

			Assert.That(result, Is.TypeOf<MethodNotAllowedResult>());
		}
	}
}