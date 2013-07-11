using System.Net.Http;
using NUnit.Framework;
using Nonae.Core.Authorization;
using Nonae.Core.Endpoints;
using Nonae.Core.Requests;
using Rhino.Mocks;

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
			var requestDetails = new RequestDetails(_endpointDetails, _credentials, HttpMethod.Options);

			Assert.That(requestDetails.Answers(HttpMethod.Options), Is.EqualTo(true));
		}

		[Test]
		public void Identifies_itself_correctly_when_not_options_request()
		{
			var requestDetails = new RequestDetails(_endpointDetails, _credentials, HttpMethod.Get);

			Assert.That(requestDetails.Answers(HttpMethod.Options), Is.EqualTo(false));
		}

		[Test]
		public void Save_calls_save_on_the_endpoint_details()
		{
			_endpointDetails = MockRepository.GenerateStub<IEndpointDetails>();
			var requestDetails = new RequestDetails(_endpointDetails, _credentials, HttpMethod.Put);

			requestDetails.Save();

			_endpointDetails.AssertWasCalled(ed => ed.Save());
		}
	}
}