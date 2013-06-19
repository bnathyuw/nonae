using NUnit.Framework;
using Nonae.Core.Endpoints;

namespace Nonae.Tests.Unit.Endpoints
{
	[TestFixture]
	public class EndpointTests
	{
		private Endpoint _endpoint;
		private const string UrlPattern = "^/resources/(?<id>\\d*)$";

		[SetUp]
		public void SetUp()
		{
			_endpoint = Endpoint.AtUrl(UrlPattern);
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
	}
}