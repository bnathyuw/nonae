Feature: Options
	In order to know what requests I can make
	As a consumer of the API
	I want to be make OPTIONS calls

Scenario: Call OPTIONS on a collection
	When I call OPTIONS on a collection
	Then I am told I can POST
