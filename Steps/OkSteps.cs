using System;
using TechTalk.SpecFlow;

namespace Nonae.Tests.EndToEnd....Steps
{
    [Binding]
    public class OkSteps
    {
        [When(@"I make a request")]
        public void WhenIMakeARequest()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the response should be (.*) OK")]
        public void ThenTheResponseShouldBeOK(int p0)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
