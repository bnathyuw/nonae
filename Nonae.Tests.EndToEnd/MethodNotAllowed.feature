Feature: NotAllowed
	In order to use the API correctly
	As a consumer of the API
	I want to be told when my suggested method is not allowed

Scenario: Call DELETE on an indelible resource
	When I call DELETE on a collection
	Then I get a 405 Method Not Allowed response
