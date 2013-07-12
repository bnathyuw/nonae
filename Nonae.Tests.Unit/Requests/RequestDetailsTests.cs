using System.Net.Http;
using NUnit.Framework;
using Nonae.Core.Authorization;
using Nonae.Core.Endpoints;
using Nonae.Core.Requests;

namespace Nonae.Tests.Unit.Requests
{
	[TestFixture]
	public class RequestDetailsTests
	{
		private IEndpointDetails _endpointDetails;
		private Credentials _credentials;

		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			_endpointDetails = null;
			_credentials = null;
		}

		[Test]
		public void Identifies_itself_correctly_when_options_request()
		{
			var requestDetails = new RequestDetails(_credentials, HttpMethod.Options);

			Assert.That(requestDetails.Answers(HttpMethod.Options), Is.EqualTo(true));
		}

		[Test]
		public void Identifies_itself_correctly_when_not_options_request()
		{
			var requestDetails = new RequestDetails(_credentials, HttpMethod.Get);

			Assert.That(requestDetails.Answers(HttpMethod.Options), Is.EqualTo(false));
		}
	}
}