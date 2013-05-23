Feature: NotFound
	In order to use the API correctly
	As a consumer of the API
	I want to be told when the resource I have request does not exist

Scenario Outline: Call verb on a non-existent url
	When I call <verb> on a silly url
	Then I get a 404 Not Found response

Scenarios:
	| verb   | url                           |
	| DELETE | a silly url                   |
	| GET    | a silly url                   |
	| HEAD   | a silly url                   |
	| POST   | a silly url                   |
	| DELETE | a resource that doesn't exist |
	| GET    | a resource that doesn't exist |
	| HEAD   | a resource that doesn't exist |
	| POST   | a resource that doesn't exist |
