using NUnit.Framework;
using SdetPractice.Configuration;

namespace SdetPractice.Tests.TestData
{
    /// <summary>Shared test data for Basic Auth scenarios used by both UI and API test suites.</summary>
    public static class BasicAuthTestData
    {
        /// <summary>Provides valid credentials that should result in a successful authentication response.</summary>
        public static IEnumerable<TestCaseData> ValidCredentials()
        {
            var settings = TestSettings.Instance;
            yield return new TestCaseData(settings.BasicAuthUsername, settings.BasicAuthPassword).SetName("Valid credentials");
        }

        /// <summary>Provides invalid credential combinations that should result in an unauthorized response.</summary>
        public static IEnumerable<TestCaseData> InvalidCredentials()
        {
            var settings = TestSettings.Instance;
            yield return new TestCaseData("wronguser"               , "wrongpass"                 ).SetName("Wrong credentials");
            yield return new TestCaseData(settings.BasicAuthUsername, "wrongpass"                 ).SetName("Valid username wrong password");
            yield return new TestCaseData("wronguser"               , settings.BasicAuthPassword  ).SetName("Wrong username valid password");
        }
    }
}
