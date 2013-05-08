using System.Net;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Nonae.Tests.EndToEnd
{
    [Binding]
// ReSharper disable UnusedMember.Global
    public class OkSteps
// ReSharper restore UnusedMember.Global
    {
	    private Response _response;

	    [When(@"I make a request")]
// ReSharper disable UnusedMember.Global
        public void WhenIMakeARequest()
// ReSharper restore UnusedMember.Global
        {
		    _response = Request.Get("http://localhost/nonae").GetResponse();

        }

	    [Then(@"the response should be 200 OK")]
// ReSharper disable UnusedMember.Global
        public void ThenTheResponseShouldBeOk()
// ReSharper restore UnusedMember.Global
	    {
		    Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
	    }
    }
}
