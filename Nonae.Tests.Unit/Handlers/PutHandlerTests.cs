using NUnit.Framework;
using Nonae.Core.Endpoints;
using Nonae.Core.Handlers;
using Nonae.Core.Requests;
using Nonae.Core.Results;
using Rhino.Mocks;

namespace Nonae.Tests.Unit.Handlers
{
	[TestFixture]
	public class PutHandlerTests
	{
		[Test]
		public void Returns_created_if_resource_is_saved()
		{
			var putHandler = new PutHandler();
			var requestDetails = MockRepository.GenerateStub<IRequestDetails>();
		    var endpointDetails = MockRepository.GenerateStub<IEndpointDetails>();
            endpointDetails.Stub(ed => ed.Save()).Return(true);

		    var result = putHandler.Handle(requestDetails, endpointDetails);

            endpointDetails.AssertWasCalled(rd => rd.Save());
			Assert.That(result, Is.TypeOf<CreatedResult>());
		}
	}
}