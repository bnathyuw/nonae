Feature: Created
	As a user of the API
	In order to be able to create objects
	I want it to work predictably

Scenario: Call PUT on a resource that does not exist
	When I call PUT on a resource that does not exist
	Then I get a 201 Created response
