using NUnit.Framework;
using Nonae.Core.Authorization;

namespace Nonae.Tests.Unit.Authorization
{
	[TestFixture]
	public class AnonymousUserTests
	{
		private Credentials _credentials;

		[SetUp]
		public void SetUp()
		{
			_credentials = Credentials.ForAnonymousUser();
		}

		[Test]
		public void Is_authenticated()
		{
			Assert.That(_credentials.Message, Is.Null);
		}

		[Test]
		public void Username_is_null()
		{
			Assert.That(_credentials.Username, Is.Null);
		}

		[Test]
		public void Is_anonymous()
		{
			Assert.That(_credentials.IsAnonymous, Is.True);
		}
	}
}