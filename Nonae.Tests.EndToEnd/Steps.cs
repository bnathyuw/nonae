using System;
using System.Collections.Generic;
using System.Net;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Nonae.Tests.EndToEnd
{
	[Binding]
// ReSharper disable UnusedMember.Global
	public class Steps
// ReSharper restore UnusedMember.Global
	{
		private Response _response;

		private static readonly Dictionary<string, string> Urls = new Dictionary<string, string>
			                                                  {
				                                                  {"the root", "http://localhost/nonae"},
				                                                  {"a collection", "http://localhost/nonae/users"},
				                                                  {"a single resource", "http://localhost/nonae/users/1"}
			                                                  };

		private static readonly Dictionary<string, Func<string, Request>> RequestFactories = new Dictionary<string, Func<string, Request>>
			                                                                                         {
				                                                                                         {"GET", Request.Get},
				                                                                                         {"OPTIONS", Request.Options}
			                                                                                         };

		[When(@"I call (.*) on (.*)")]
// ReSharper disable UnusedMember.Global
		public void WhenICallOptionsOnACollection(string httpMethod, string resourceKey)
// ReSharper restore UnusedMember.Global
		{
			var createRequest = RequestFactories[httpMethod];
			var url = Urls[resourceKey];
			_response = createRequest(url).GetResponse();
		}

		[Then(@"I get a (.*) .* response")]
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

		[When(@"I make a request")]
		// ReSharper disable UnusedMember.Global
		public void WhenIMakeARequest()
		// ReSharper restore UnusedMember.Global
		{
			_response = Request.Get("http://localhost/nonae").GetResponse();

		}
	}
}
