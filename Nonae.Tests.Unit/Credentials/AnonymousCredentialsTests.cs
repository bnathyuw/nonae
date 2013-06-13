using NUnit.Framework;
using Nonae.Core.Credentials;

namespace Nonae.Tests.Unit.Credentials
{
	[TestFixture]
	public class AnonymousCredentialsTests
	{
		private AnonymousCredentials _credentials;

		[SetUp]
		public void SetUp()
		{
			_credentials = new AnonymousCredentials();
		}

		[Test]
		public void Is_authenticated()
		{
			Assert.That(_credentials.IsAuthenticated, Is.True);
		}

		[Test]
		public void Authorization_method_is_supported()
		{
			Assert.That(_credentials.AuthorizationMethodIsSupported, Is.True);
		}

		[Test]
		public void Username_is_null()
		{
			Assert.That(_credentials.Username, Is.Null);
		}
	}
}