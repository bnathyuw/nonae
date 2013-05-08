using System.Net;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Nonae.Tests.EndToEnd
{
	[Binding]
// ReSharper disable UnusedMember.Global
	public class Steps
	{
		private readonly Context _context;

		public Steps(Context context)
		{
			_context = context;
		}

		[When(@"I call (.*) on (.*)")]
		public void WhenICallOptionsOnACollection(string httpMethod, string resourceKey)
		{
			var createRequest = Configuration.RequestFactories[httpMethod];
			var url = Configuration.Urls[resourceKey];
			_context.Request = createRequest(url);
		}

		[Then(@"I get a (.*) .* response")]
		public void ThenIGetSuchAndSuchAResponse(HttpStatusCode expectedStatusCode)
		{
			Assert.That(_context.Response.StatusCode, Is.EqualTo(expectedStatusCode));
		}


		[Then(@"I am told I can (.*)")]
		public void ThenIAmToldICanDo(string verb)
		{
			Assert.That(_context.Response.Allow, Has.Member(verb));
		}

		[Then(@"I am not told I can (.*)")]
		public void ThenIAmNotToldICanDo(string verb)
		{
			Assert.That(_context.Response.Allow, Has.No.Member(verb));
		}
	}
// ReSharper restore UnusedMember.Global
}
