using System.Net.Http;
using NUnit.Framework;
using Nonae.Core.Endpoints;

namespace Nonae.Tests.Unit.Endpoints
{
	[TestFixture]
	public class EndpointTests
	{
		private Endpoint _nullEndpoint;
		private Endpoint _endpoint;
		private const string UrlPattern = "^/resources/(?<id>\\d*)$";

		[Test]
		public void Null_endpoint_does_not_exist()
		{
			Assert.That(_nullEndpoint.Exists, Is.False);
		}

		[SetUp]
		public void SetUp()
		{
			_nullEndpoint = Endpoint.Null();
			_endpoint = Endpoint.AtUrl(UrlPattern);
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

		[Test]
		public void Endpoint_is_authorized_for_any_credentials()
		{
			Assert.That(_endpoint.IsAuthorizedFor(null), Is.True);
		}

		[Test]
		public void Endpoint_is_at_url_that_matches_given_pattern()
		{
			Assert.That(_endpoint.IsAt("/resources/123"), Is.True);
		}

		[Test]
		public void Endpoint_is_not_at_a_url_that_does_not_match_given_pattern()
		{
			Assert.That(_endpoint.IsAt("/resources/abc"), Is.False);
		}

		[Test]
		public void Endpoint_with_no_methods_configured_has_empty_allow_header()
		{
			Assert.That(_endpoint.AllowHeader, Is.EqualTo(""));
		}

		[Test]
		public void Endpoint_with_no_methods_configured_does_not_allow_any_methods()
		{
			Assert.That(_endpoint.Allows(HttpMethod.Delete), Is.False);
			Assert.That(_endpoint.Allows(HttpMethod.Get), Is.False);
			Assert.That(_endpoint.Allows(HttpMethod.Head), Is.False);
			Assert.That(_endpoint.Allows(HttpMethod.Post), Is.False);
			Assert.That(_endpoint.Allows(HttpMethod.Put), Is.False);
		}

		[Test]
		public void Endpoint_with_configured_methods_has_correct_allow_header()
		{
			var endpoint = _endpoint.WithMethods(HttpMethod.Get, HttpMethod.Delete);

			Assert.That(endpoint.AllowHeader, Is.StringContaining("DELETE"));
			Assert.That(endpoint.AllowHeader, Is.StringContaining("GET"));
			Assert.That(endpoint.AllowHeader, Is.Not.StringContaining("HEAD"));
			Assert.That(endpoint.AllowHeader, Is.Not.StringContaining("POST"));
			Assert.That(endpoint.AllowHeader, Is.Not.StringContaining("PUT"));
		}

		[Test]
		public void Endpoint_with_configured_methods_allows_correct_methods()
		{
			var endpoint = _endpoint.WithMethods(HttpMethod.Get, HttpMethod.Delete); 
			
			Assert.That(endpoint.Allows(HttpMethod.Delete), Is.True);
			Assert.That(endpoint.Allows(HttpMethod.Get), Is.True);
			Assert.That(endpoint.Allows(HttpMethod.Head), Is.False);
			Assert.That(endpoint.Allows(HttpMethod.Post), Is.False);
			Assert.That(endpoint.Allows(HttpMethod.Put), Is.False);
		}
	}
}