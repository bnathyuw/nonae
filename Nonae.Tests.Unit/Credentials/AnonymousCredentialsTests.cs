﻿using NUnit.Framework;
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