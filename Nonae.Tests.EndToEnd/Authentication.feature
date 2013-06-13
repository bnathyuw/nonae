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
	And I specify username 'username' and password 'not the password' for Basic authentication
	Then I get a 401 Unauthorized response
	And the reason is 'User Not Found'
	And I get a WWW-Authenticate header requesting Basic authentication

Scenario: Incorrect authentication method
	When I call GET on the root
	And I specify username 'username' and password 'password' for Dodgy authentication
	Then I get a 401 Unauthorized response
	And the reason is 'Unsupported Authorization Method'

Scenario: Correct credentials
	When I call GET on the root
	And I specify username 'username' and password 'password' for Basic authentication
	Then I get a 200 OK response

Scenario: Sufficient privileges
	When I call GET on a protected resource
	And I specify username 'admin' and password 'password' for Basic authentication
	Then I get a 200 OK response

Scenario: Insufficient privileges
	When I call GET on a protected resource
	And I specify username 'username' and password 'password' for Basic authentication
	Then I get a 401 Unauthorized response
	And the reason is 'Insufficient privileges'