using NUnit.Framework;
using Nonae.Core.Endpoints;

namespace Nonae.Tests.Unit.Endpoints
{
	[TestFixture]
	public class EndpointStoreTests
	{
		private EndpointStore _endpointStore;

		[SetUp]
		public void SetUp()
		{
			_endpointStore = new EndpointStore();
		}

		[Test]
		public void Returns_endpoint_when_it_exists()
		{
			var createdEndpoint = Endpoint.AtUrl("/test/url");
			_endpointStore.Add(createdEndpoint);

			var foundEndpoint = _endpointStore.Get("/test/url");

			Assert.That(foundEndpoint, Is.EqualTo(createdEndpoint));
		}

		[Test]
		public void Returns_null_endpoint_when_it_does_not_exist()
		{
			var foundEndpoint = _endpointStore.Get("/test/url");

			Assert.That(foundEndpoint.Exists, Is.False);
		}
	}
}