using Allure.NUnit;
using Allure.NUnit.Attributes;
using System.Net;
using FluentAssertions;
using SdetPractice.Configuration;
using SdetPractice.Tests.TestData;
using SdetPractice.Utilities;

namespace SdetPractice.Tests.API
{
    /// <summary>API test suite for the Basic Auth endpoint. Validates HTTP status codes using HttpClient — no browser required.</summary>
    [TestFixture]
    [AllureNUnit]
    [AllureSuite("API Tests")]
    [AllureFeature("Basic Authentication")]
    public class BasicAuthApiTests
    {
        [Test]
        [Description("TC011 - Validate 200 is returned when valid credentials are provided")]
        public async Task BasicAuth_WithValidCredentials_ShouldReturn200()
        {
            var settings = TestSettings.Instance;
            var url = $"{settings.BaseUrl}/basic_auth";
            var statusCode = await BasicAuthClient.GetStatusCodeAsync(url, settings.BasicAuthUsername, settings.BasicAuthPassword);

            statusCode.Should().Be(HttpStatusCode.OK,
                "server should return 200 when valid credentials are provided");
        }

        [Test]
        [Description("TC012 - Validate 401 is returned when no credentials are provided")]
        public async Task BasicAuth_WithNoCredentials_ShouldReturn401()
        {
            var url = $"{TestSettings.Instance.BaseUrl}/basic_auth";
            var statusCode = await BasicAuthClient.GetStatusCodeAsync(url);

            statusCode.Should().Be(HttpStatusCode.Unauthorized,
                "server should reject the request when no credentials are provided");
        }

        [TestCaseSource(typeof(BasicAuthTestData), nameof(BasicAuthTestData.InvalidCredentials))]
        public async Task BasicAuth_WithInvalidCredentials_ShouldReturn401(string username, string password)
        {
            var url = $"{TestSettings.Instance.BaseUrl}/basic_auth";
            var statusCode = await BasicAuthClient.GetStatusCodeAsync(url, username, password);

            statusCode.Should().Be(HttpStatusCode.Unauthorized,
                "server should reject the request when invalid credentials are provided");
        }
    }
}
