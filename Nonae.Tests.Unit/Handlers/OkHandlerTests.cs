using NUnit.Framework;
using Nonae.Core.Endpoints;
using Nonae.Core.Handlers;
using Nonae.Core.Requests;
using Nonae.Core.Results;
using Rhino.Mocks;

namespace Nonae.Tests.Unit.Handlers
{
	[TestFixture]
	public class OkHandlerTests
	{
		[Test]
		public void Returns_ok_result()
		{
			var okHandler = new OkHandler();

			var requestDetails = MockRepository.GenerateStub<IRequestDetails>();
		    var endpointDetails = MockRepository.GenerateStub<IEndpointDetails>();
			var result = okHandler.Handle(requestDetails, endpointDetails);

			Assert.That(result, Is.TypeOf<OkResult>());
		}
	}
}