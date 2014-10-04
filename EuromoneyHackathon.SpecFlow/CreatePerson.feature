Feature: RateEvent
	In order to influence future events
	As a User
	I want to rate this event

@mytag
Scenario: Rate event
	Given a User attends an event
	And the User is logged in
	When the User clicks Rate
	Then the User should see a notification confirming rating
