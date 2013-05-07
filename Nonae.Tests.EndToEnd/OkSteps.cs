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
	    private Request _request;

	    [When(@"I make a request")]
// ReSharper disable UnusedMember.Global
        public void WhenIMakeARequest()
// ReSharper restore UnusedMember.Global
        {
	        var request = Request.Get("http://localhost/nonae/");
	        _request = request;
        }

	    [Then(@"the response should be 200 OK")]
// ReSharper disable UnusedMember.Global
        public void ThenTheResponseShouldBeOk()
// ReSharper restore UnusedMember.Global
	    {
		    var response = _request.GetResponse();
		    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
	    }
    }
}
