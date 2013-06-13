using NUnit.Framework;
using Nonae.Core.Authorization;

namespace Nonae.Tests.Unit.Authorization
{
	[TestFixture]
	public class AuthenticatedUserTests
	{
		private const string Username = "foo";
		private Credentials _credentials;

		[SetUp]
		public void SetUp()
		{
			_credentials = Credentials.ForAuthenticatedUser(Username);
		}

		[Test]
		public void Is_authenticated()
		{
			Assert.That(_credentials.Message, Is.Null);
		}

		[Test]
		public void Username_is_correct()
		{
			Assert.That(_credentials.Username, Is.EqualTo(Username));
		}

		[Test]
		public void Is_anonymous()
		{
			Assert.That(_credentials.IsAnonymous, Is.False);
		}
	}
}