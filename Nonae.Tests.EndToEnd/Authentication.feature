Feature: Authorisation
	In order to prevent illicit access to resources
	As a guardian of the site
	I want to authenticate

Scenario: Anonymous access
	When I call GET on the root
	And I do not specify credentials
	Then I get a 200 OK response

Scenario: Incorrect credentials
	When I call GET on the root
	And I specify username 'username' and password 'not the password'
	Then I get a 401 Unauthorized response
	And I get a WWW-Authenticate header requesting Basic authentication

Scenario: Correct credentials
	When I call GET on the root
	And I specify username 'username' and password 'password'
	Then I get a 200 OK response