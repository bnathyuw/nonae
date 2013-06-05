﻿using System.Net;
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

		[When(@"I call ([A-Z]*) on (.*)")]
		public void WhenICallOptionsOnACollection(string httpMethod, string resourceKey)
		{
			var createRequest = Configuration.RequestFactories[httpMethod];
			var url = Configuration.Urls[resourceKey];
			_context.Request = createRequest(url);
		}

		[When(@"I do not specify credentials")]
		public void WhenIDoNotSpecifyCredentials()
		{
			
		}

		[When(@"I specify username '(.*)' and password '(.*)'")]
		public void WhenISpecifyUsernameAndPassword(string username, string password)
		{
			_context.SetBasicAuthentication(username, password);
		}


		[Then(@"I get a (\d*) .* response")]
		public void ThenIGetSuchAndSuchAResponse(HttpStatusCode expectedStatusCode)
		{
			Assert.That(_context.Response.StatusCode, Is.EqualTo(expectedStatusCode));
		}


		[Then(@"I am told I can ([A-Z]*)")]
		public void ThenIAmToldICanDo(string verb)
		{
			Assert.That(_context.Response.Allow, Has.Member(verb));
		}

		[Then(@"I am not told I can ([A-Z]*)")]
		public void ThenIAmNotToldICanDo(string verb)
		{
			Assert.That(_context.Response.Allow, Has.No.Member(verb));
		}
	}
// ReSharper restore UnusedMember.Global
}
