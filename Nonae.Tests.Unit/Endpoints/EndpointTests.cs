using System.Net.Http;
using NUnit.Framework;
using Nonae.Core.Endpoints;

namespace Nonae.Tests.Unit.Endpoints
{
	[TestFixture]
	public class EndpointTests
	{
		private Endpoint _nullEndpoint;

		[Test]
		public void Null_endpoint_does_not_exist()
		{
			Assert.That(_nullEndpoint.Exists, Is.False);
		}

		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			_nullEndpoint = Endpoint.Null();
		}

		[Test]
		public void Null_endpoint_is_authorized_for_any_credentials()
		{
			Assert.That(_nullEndpoint.IsAuthorizedFor(null), Is.True);
		}

		[Test]
		public void Null_endpoint_has_empty_allow_header()
		{
			Assert.That(_nullEndpoint.AllowHeader, Is.EqualTo(""));
		}

		[Test]
		public void Null_endpoint_does_not_allow_any_methods()
		{
			Assert.That(_nullEndpoint.Allows(HttpMethod.Delete), Is.False);
			Assert.That(_nullEndpoint.Allows(HttpMethod.Get), Is.False);
			Assert.That(_nullEndpoint.Allows(HttpMethod.Head), Is.False);
			Assert.That(_nullEndpoint.Allows(HttpMethod.Post), Is.False);
			Assert.That(_nullEndpoint.Allows(HttpMethod.Put), Is.False);
		}
	}
}