using Allure.NUnit.Attributes;
using FluentAssertions;
using SdetPractice.Base;
using SdetPractice.Pages;

namespace SdetPractice.Tests.UI
{
    /// <summary>Test suite for the AB Testing page.</summary>
    [TestFixture]
    [AllureSuite("UI Tests")]
    [AllureFeature("AB Testing")]
    public class ABTestingUITests : BaseTest
    {
        private ABTestingPage _page = null!;

        /// <summary>Opens the AB Testing page before each test.</summary>
        [SetUp]
        public void SetUpPage()
        {
            _page = new ABTestingPage(Driver);
            _page.Open();
        }

        [Test]
        [Description("TC001 - Validate AB Testing page loads and displays correct content")]
        public void ABTestingPage_ShouldLoadAndDisplayCorrectContent()
        {
            _page.IsLoaded()
                .Should().BeTrue("AB Testing page should load successfully");

            _page.GetHeadingText()
                .Should().ContainAny("A/B Test", "No A/B Test");

            _page.GetDescription()
                .Should().NotBeNullOrEmpty("Description should be displayed on AB Testing page");
        }
    }
}