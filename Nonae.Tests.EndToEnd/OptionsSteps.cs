using System.Net;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Nonae.Tests.EndToEnd
{
	[Binding]
// ReSharper disable UnusedMember.Global
	public class OptionsSteps
// ReSharper restore UnusedMember.Global
	{
		private Response _response;

		[When(@"I call OPTIONS on a collection")]
// ReSharper disable UnusedMember.Global
		public void WhenICallOptionsOnACollection()
// ReSharper restore UnusedMember.Global
		{
			_response = Request.Options("http://localhost/nonae/users").GetResponse();
		}

		[When(@"I call OPTIONS on a single resource")]
// ReSharper disable UnusedMember.Global
		public void WhenICallOptionsOnASingleResource()
// ReSharper restore UnusedMember.Global
		{
			_response = Request.Options("http://localhost/nonae/users/1").GetResponse();
		}

		[Then(@"I get a (.*) OK response")]
// ReSharper disable UnusedMember.Global
		public void ThenIGetSuchAndSuchAResponse(HttpStatusCode expectedStatusCode)
// ReSharper restore UnusedMember.Global
		{
			Assert.That(_response.StatusCode, Is.EqualTo(expectedStatusCode));
		}


		[Then(@"I am told I can (.*)")]
// ReSharper disable UnusedMember.Global
		public void ThenIAmToldICanDo(string verb)
// ReSharper restore UnusedMember.Global
		{
			Assert.That(_response.Allow, Is.StringContaining(verb));
		}
	}
}
