﻿Feature: Ok
	As a user of the API
	In order to use it correctly
	I want to know when everything has gone to plan

Scenario: Call GET on the root
	When I call GET on the root
	Then I get a 200 OK response
