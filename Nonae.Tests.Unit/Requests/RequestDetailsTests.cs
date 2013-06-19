using System.Net.Http;
using System.Web;
using NUnit.Framework;
using Nonae.Core.Authorization;
using Nonae.Core.Endpoints;
using Nonae.Core.Requests;

namespace Nonae.Tests.Unit.Requests
{
	[TestFixture]
	public class RequestDetailsTests
	{
		private EndpointDetails _endpointDetails;
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
			var requestDetails = new RequestDetails(_endpointDetails, _credentials, HttpMethod.Options);

			Assert.That(requestDetails.IsOptionsRequest, Is.EqualTo(true));
		}

		[Test]
		public void Identifies_itself_correctly_when_not_options_request()
		{
			var requestDetails = new RequestDetails(_endpointDetails, _credentials, HttpMethod.Get);

			Assert.That(requestDetails.IsOptionsRequest, Is.EqualTo(false));
		}
	}
}