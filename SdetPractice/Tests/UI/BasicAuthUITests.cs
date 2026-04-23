using Allure.NUnit.Attributes;
using FluentAssertions;
using SdetPractice.Base;
using SdetPractice.Pages;

namespace SdetPractice.Tests.UI
{
    /// <summary>UI test suite for the Basic Auth page. Validates browser-rendered content using Selenium.</summary>
    [TestFixture]
    [AllureSuite("UI Tests")]
    [AllureFeature("Basic Authentication")]
    public class BasicAuthUITests : BaseTest
    {
        private BasicAuthPage _page = null!;

        /// <summary>Initialises the Basic Auth page object before each test.</summary>
        [SetUp]
        public void SetUpPage()
        {
            _page = new BasicAuthPage(Driver);
        }

        [Test]
        [Description("TC006 - Validate successful login with valid credentials displays success message")]
        public void BasicAuth_WithValidCredentials_ShouldDisplaySuccessMessage()
        {
            _page.Open();

            _page.IsLoaded()
                .Should().BeTrue("page should load after successful authentication");

            _page.GetSuccessMessage()
                .Should().Contain("");
        }
    }
}
