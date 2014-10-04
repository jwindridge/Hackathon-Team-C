using System;
using TechTalk.SpecFlow;

namespace EuromoneyHackathon.SpecFlow.Steps
{
    [Binding]
    public class RateEventSteps
    {
        [Given(@"a User attends an event")]
public void GivenAUserAttendsAnEvent()
{
    ScenarioContext.Current.Pending();
}

        [Given(@"the User is logged in")]
public void GivenTheUserIsLoggedIn()
{
    ScenarioContext.Current.Pending();
}

        [When(@"the User clicks Rate")]
public void WhenTheUserClicksRate()
{
    ScenarioContext.Current.Pending();
}

        [Then(@"the User should see a notification confirming rating")]
public void ThenTheUserShouldSeeANotificationConfirmingRating()
{
    ScenarioContext.Current.Pending();
}
    }
}
