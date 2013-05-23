Feature: NotFound
	In order to use the API correctly
	As a consumer of the API
	I want to be told when the resource I have request does not exist

@inprogress
Scenario: Call GET on a silly url
	When I call GET on a silly url
	Then I get a 404 Not Found response