using NUnit.Framework;
using Nonae.Core.Credentials;

namespace Nonae.Tests.Unit.Credentials
{
	[TestFixture]
	public class InvalidCredentialsTests
	{
		private InvalidCredentials _credentials;
		private const string FailureMessage = "Something has gone wrong";

		[SetUp]
		public void SetUp()
		{
			_credentials = new InvalidCredentials(FailureMessage);
		}

		[Test]
		public void Is_not_authenticated()
		{
			Assert.That(_credentials.Message, Is.EqualTo(FailureMessage));
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