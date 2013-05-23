Feature: Options
	In order to know what requests I can make
	As a consumer of the API
	I want to be make OPTIONS calls

Scenario Outline: Call OPTIONS on an endpoint
	When I call OPTIONS on <endpoint>
	Then I get a 200 OK response
	And I am <status> I can <verb>

Scenarios:
	| endpoint          | status   | verb   |
	| a collection      | told     | POST   |
	| a collection      | told     | GET    |
	| a collection      | told     | HEAD   |
	| a collection      | not told | DELETE |
	| a collection      | not told | PUT    |
	| a single resource | told     | GET    |
	| a single resource | told     | PUT    |
	| a single resource | told     | HEAD   |
	| a single resource | told     | DELETE |
	| a single resource | not told | POST   |
	| the root          | told     | GET    |
	| the root          | told     | HEAD   |
	| the root          | not told | DELETE |
	| the root          | not told | PUT    |
	| the root          | not told | POST   |
	| a silly url       | not told | DELETE |
	| a silly url       | not told | GET    |
	| a silly url       | not told | HEAD   |
	| a silly url       | not told | PUT    |
	| a silly url       | not told | POST   |