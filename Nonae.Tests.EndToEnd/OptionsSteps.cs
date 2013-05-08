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

		[Then(@"I am told I can POST")]
// ReSharper disable UnusedMember.Global
		public void ThenIAmToldICanPost()
// ReSharper restore UnusedMember.Global
		{
			Assert.That(_response.Allow, Is.StringContaining("POST"));
		}
	}
}
