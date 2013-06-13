using NUnit.Framework;
using Nonae.Core.Credentials;

namespace Nonae.Tests.Unit.Credentials
{
	[TestFixture]
	public class BasicCredentialsTests
	{
		private const string Username = "foo";
		private BasicCredentials _credentials;

		[SetUp]
		public void SetUp()
		{
			_credentials = new BasicCredentials(Username);
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