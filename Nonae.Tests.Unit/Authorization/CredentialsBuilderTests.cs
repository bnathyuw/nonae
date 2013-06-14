using System;
using System.Text;
using NUnit.Framework;
using Nonae.Core.Authorization;
using Rhino.Mocks;

namespace Nonae.Tests.Unit.Authorization
{
	[TestFixture]
	public class CredentialsBuilderTests
	{
		private const string Username = "username";
		private const string Password = "password";
		private IAuthenticationProvider _authenticationProvider;
		private CredentialsBuilder _credentialsBuilder;
		private string _encodedCredentials;

		[SetUp]
		public void SetUp()
		{
			_authenticationProvider = MockRepository.GenerateStub<IAuthenticationProvider>();
			_credentialsBuilder = new CredentialsBuilder(_authenticationProvider);
			_encodedCredentials = Encode(Username, Password);
		}

		[Test]
		public void Returns_anonymous_user_when_no_authorization_header_is_given()
		{
			var credentials = _credentialsBuilder.From(null);

			Assert.That(credentials.IsAnonymous, Is.True);
			Assert.That(credentials.Username, Is.Null);
			Assert.That(credentials.IsAuthenticated, Is.True);
			Assert.That(credentials.FailureMessage, Is.Null);
		}

		[Test]
		public void Returns_unsupported_authorization_method_when_authorization_method_is_not_supported()
		{
			var credentials = _credentialsBuilder.From("Unsupported blah");

			Assert.That(credentials.IsAnonymous, Is.False);
			Assert.That(credentials.Username, Is.Null);
			Assert.That(credentials.IsAuthenticated, Is.False);
			Assert.That(credentials.FailureMessage, Is.EqualTo("Unsupported Authorization Method"));
		}

		[Test]
		public void Returns_user_not_found_when_authentication_provider_finds_no_user()
		{
			_authenticationProvider.Stub(ap => ap.Authenticate(Username, Password)).IgnoreArguments().Return(false);

			var credentials = _credentialsBuilder.From("Basic " + _encodedCredentials);

			_authenticationProvider.AssertWasCalled(ap => ap.Authenticate(Username, Password));
			Assert.That(credentials.IsAnonymous, Is.False);
			Assert.That(credentials.Username, Is.Null);
			Assert.That(credentials.IsAuthenticated, Is.False);
			Assert.That(credentials.FailureMessage, Is.EqualTo("User Not Found"));
		}

		[Test]
		public void Returns_authenticated_user_when_authentication_provider_finds_the_user()
		{
			_authenticationProvider.Stub(ap => ap.Authenticate(Username, Password)).IgnoreArguments().Return(true);

			var credentials = _credentialsBuilder.From("Basic " + _encodedCredentials);

			_authenticationProvider.AssertWasCalled(ap => ap.Authenticate(Username, Password));
			Assert.That(credentials.IsAnonymous, Is.False);
			Assert.That(credentials.Username, Is.EqualTo(Username));
			Assert.That(credentials.IsAuthenticated, Is.True);
			Assert.That(credentials.FailureMessage, Is.Null);
		}

		private static string Encode(string username, string password)
		{
			var text = String.Format("{0}:{1}", username, password);
			var bytes = Encoding.Unicode.GetBytes(text);
			return Convert.ToBase64String(bytes);
		}
	}
}