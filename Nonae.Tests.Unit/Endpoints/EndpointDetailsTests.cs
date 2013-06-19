using System.Collections.Generic;
using System.Net.Http;
using NUnit.Framework;
using Nonae.Core.Endpoints;
using Nonae.Core.Handlers;
using Rhino.Mocks;

namespace Nonae.Tests.Unit.Endpoints
{
	[TestFixture]
	public class EndpointDetailsTests
	{
		private EndpointDetails _nullEndpointDetails;
		private EndpointDetails _endpointDetails;
		private IResourceRepository _resourceRepository;
		private const string UrlPattern = "^/resources/(?<id>\\d*)$";
		private const string Path = "/resources/123";

		[Test]
		public void Null_endpoint_does_not_exist()
		{
			Assert.That(_nullEndpointDetails.Exists, Is.False);
		}

		[SetUp]
		public void SetUp()
		{
			_nullEndpointDetails = EndpointDetails.Null();
			_resourceRepository = MockRepository.GenerateStub<IResourceRepository>();
			_resourceRepository.Stub(r => r.Exists(Arg<Dictionary<string, string>>.Matches(d => d["id"] == "123"))).Return(true);
			_endpointDetails = new EndpointDetails(UrlPattern, new List<HttpMethod> { HttpMethod.Get, HttpMethod.Delete }, credentials => true, _resourceRepository, Path);
		}

		[Test]
		public void Null_endpoint_is_authorized_for_any_credentials()
		{
			Assert.That(_nullEndpointDetails.IsAuthorizedFor(null), Is.True);
		}

		[Test]
		public void Null_endpoint_has_empty_allow_header()
		{
			Assert.That(_nullEndpointDetails.AllowHeader, Is.EqualTo(""));
		}

		[Test]
		public void Null_endpoint_does_not_allow_any_methods()
		{
			Assert.That(_nullEndpointDetails.Allows(HttpMethod.Delete), Is.False);
			Assert.That(_nullEndpointDetails.Allows(HttpMethod.Get), Is.False);
			Assert.That(_nullEndpointDetails.Allows(HttpMethod.Head), Is.False);
			Assert.That(_nullEndpointDetails.Allows(HttpMethod.Post), Is.False);
			Assert.That(_nullEndpointDetails.Allows(HttpMethod.Put), Is.False);
		}

		[Test]
		public void Endpoint_is_authorized_for_any_credentials()
		{
			Assert.That(_endpointDetails.IsAuthorizedFor(null), Is.True);
		}

		[Test]
		public void Endpoint_with_no_methods_configured_has_empty_allow_header()
		{
			var endpoint = new EndpointDetails(UrlPattern, new List<HttpMethod>(), credentials => true, _resourceRepository, Path);
			
			Assert.That(endpoint.AllowHeader, Is.EqualTo(""));
		}

		[Test]
		public void Endpoint_with_no_methods_configured_does_not_allow_any_methods()
		{
			var endpoint = new EndpointDetails(UrlPattern, new List<HttpMethod>(), credentials => true, _resourceRepository, Path);
			
			Assert.That(endpoint.Allows(HttpMethod.Delete), Is.False);
			Assert.That(endpoint.Allows(HttpMethod.Get), Is.False);
			Assert.That(endpoint.Allows(HttpMethod.Head), Is.False);
			Assert.That(endpoint.Allows(HttpMethod.Post), Is.False);
			Assert.That(endpoint.Allows(HttpMethod.Put), Is.False);
		}

		[Test]
		public void Endpoint_with_configured_methods_has_correct_allow_header()
		{
			Assert.That(_endpointDetails.AllowHeader, Is.StringContaining("DELETE"));
			Assert.That(_endpointDetails.AllowHeader, Is.StringContaining("GET"));
			Assert.That(_endpointDetails.AllowHeader, Is.Not.StringContaining("HEAD"));
			Assert.That(_endpointDetails.AllowHeader, Is.Not.StringContaining("POST"));
			Assert.That(_endpointDetails.AllowHeader, Is.Not.StringContaining("PUT"));
		}

		[Test]
		public void Endpoint_with_configured_methods_allows_correct_methods()
		{
			Assert.That(_endpointDetails.Allows(HttpMethod.Delete), Is.True);
			Assert.That(_endpointDetails.Allows(HttpMethod.Get), Is.True);
			Assert.That(_endpointDetails.Allows(HttpMethod.Head), Is.False);
			Assert.That(_endpointDetails.Allows(HttpMethod.Post), Is.False);
			Assert.That(_endpointDetails.Allows(HttpMethod.Put), Is.False);
		}

		[Test]
		public void Endpoint_with_no_repository_has_existent_resource()
		{
			Assert.That(_endpointDetails.ResourceExists, Is.True);
		}

		[Test]
		public void Endpoint_with_repository_checks_repository_to_see_if_resource_exists()
		{
			var resourceExists = _endpointDetails.ResourceExists;

			_resourceRepository.AssertWasCalled(r => r.Exists(Arg<Dictionary<string,string>>.Matches(d => d["id"] == "123")));
			Assert.That(resourceExists, Is.True);
		}
	}
}