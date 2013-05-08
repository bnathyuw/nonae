Feature: Options
	In order to know what requests I can make
	As a consumer of the API
	I want to be make OPTIONS calls

Scenario: Call OPTIONS on a collection
	When I call OPTIONS on a collection
	Then I am told I can POST

Scenario: Call OPTIONS on a single resource
	When I call OPTIONS on a single resource
	Then I am told I can GET
	And I am told I can PUT
	And I am told I can HEAD
	And I am told I can DELETE
