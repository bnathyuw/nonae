Feature: NotFound
	In order to use the API correctly
	As a consumer of the API
	I want to be told when the resource I have request does not exist

Scenario Outline: Call verb on a silly url
	When I call <verb> on a silly url
	Then I get a 404 Not Found response
	And the reason is 'Unknown Address'

Scenarios:
	| verb   |
	| DELETE |
	| GET    |
	| POST   |
	| PUT    |

Scenario: Call HEAD on a silly url
	When I call HEAD on a silly url
	Then I get a 404 Not Found response

Scenario Outline: Call verb on a resource that does not exist
	When I call <verb> on a resource that does not exist
	Then I get a 404 Not Found response
	And the reason is 'Resource Not Found'

Scenarios:
	| verb   |
	| DELETE |
	| GET    |

Scenario: Call HEAD on a resource that does not exist
	When I call HEAD on a resource that does not exist
	Then I get a 404 Not Found response

Scenario: Call put on a resource that does not exist
	When I call PUT on a resource that does not exist
	Then I do not get a 404 Not Found response