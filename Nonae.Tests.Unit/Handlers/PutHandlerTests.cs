using NUnit.Framework;
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

			var result = putHandler.Handle(requestDetails);

			Assert.That(result, Is.TypeOf<CreatedResult>());
		}
	}
}