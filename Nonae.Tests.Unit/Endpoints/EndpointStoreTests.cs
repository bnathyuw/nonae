using System.Collections.Generic;
using NUnit.Framework;
using Nonae.Core.Endpoints;
using Nonae.Core.Handlers;
using Rhino.Mocks;

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

		// TODO: split this up
		[Test]
		public void Calls_repository_with_expected_parts()
		{
			var resourceRepository = MockRepository.GenerateStub<IResourceRepository>();
			var createdEndpoint = Endpoint.AtUrl("^/test/(?<Id>\\d+)/me/(?<Id2>.+)$").StoredAt(resourceRepository);
			_endpointStore.Add(createdEndpoint);
			resourceRepository.Stub(rr => rr.Exists(Arg<Dictionary<string, string>>.Matches(a => a["Id"] == "123" && a["Id2"] == "abc"))).Return(true);

			var foundEndpoint = _endpointStore.Get("/test/123/me/abc");
			var resourceExists = foundEndpoint.ResourceExists;

			resourceRepository.AssertWasCalled(rr => rr.Exists(Arg<Dictionary<string, string>>.Matches(a => a["Id"] == "123" && a["Id2"] == "abc")));
			Assert.That(resourceExists, Is.True);
		}

		[Test]
		public void Returns_null_endpoint_when_it_does_not_exist()
		{
			var foundEndpoint = _endpointStore.Get("/test/url");

			Assert.That(foundEndpoint.Exists, Is.False);
		}
	}
}