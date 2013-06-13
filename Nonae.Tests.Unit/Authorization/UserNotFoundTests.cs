using NUnit.Framework;
using Nonae.Core.Authorization;

namespace Nonae.Tests.Unit.Authorization
{
	[TestFixture]
	public class UserNotFoundTests
	{
		private Credentials _credentials;

		[SetUp]
		public void SetUp()
		{
			_credentials = Credentials.ForUserNotFound();
		}

		[Test]
		public void Is_not_authenticated()
		{
			Assert.That(_credentials.Message, Is.EqualTo("User Not Found"));
		}

		[Test]
		public void Username_is_null()
		{
			Assert.That(_credentials.Username, Is.Null);
		}

		[Test]
		public void Is_not_anonymous()
		{
			Assert.That(_credentials.IsAnonymous, Is.False);
		}
	}
}