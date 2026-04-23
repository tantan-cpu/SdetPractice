using Allure.NUnit.Attributes;
using FluentAssertions;
using SdetPractice.Base;
using SdetPractice.Pages;

namespace SdetPractice.Tests.UI
{
    /// <summary>UI test suite for the Broken Images page. Validates image load status via JavaScript naturalWidth.</summary>
    [TestFixture]
    [AllureSuite("UI Tests")]
    [AllureFeature("Broken Images")]
    public class BrokenImagesUITests : BaseTest
    {
        private BrokenImagesPage _page = null!;

        /// <summary>Opens the Broken Images page before each test.</summary>
        [SetUp]
        public void SetUpPage()
        {
            _page = new BrokenImagesPage(Driver);
            _page.Open();
        }

        [Test]
        [Description("TC016 - Validate Broken Images page loads successfully")]
        public void BrokenImagesPage_ShouldLoadSuccessfully()
        {
            _page.IsLoaded()
                .Should().BeTrue("page should load with the heading visible");
        }

        [Test]
        [Description("TC017 - Validate total image count on the page")]
        public void BrokenImagesPage_ShouldHaveCorrectTotalImageCount()
        {
            _page.GetTotalImageCount()
                .Should().Be(3, "page should contain exactly 3 images");
        }

        [Test]
        [Description("TC018 - Validate broken images are detected on the page")]
        public void BrokenImagesPage_ShouldDetectBrokenImages()
        {
            _page.GetBrokenImageCount()
                .Should().Be(2, "page should contain exactly 2 broken images");
        }

        [Test]
        [Description("TC019 - Validate broken image sources are correct")]
        public void BrokenImagesPage_ShouldReturnCorrectBrokenImageSources()
        {
            var brokenSources = _page.GetBrokenImageSources();

            brokenSources.Should().HaveCount(2, "there should be exactly 2 broken images");

            brokenSources.Should().ContainMatch("*asdf.jpg", "asdf.jpg should be a broken image");
            brokenSources.Should().ContainMatch("*hjkl.jpg", "hjkl.jpg should be a broken image");
        }

        [Test]
        [Description("TC020 - Validate valid image is not reported as broken")]
        public void BrokenImagesPage_ValidImage_ShouldNotBeReportedAsBroken()
        {
            var brokenSources = _page.GetBrokenImageSources();

            brokenSources.Should().NotContainMatch("*avatar-blank.jpg",
                "avatar-blank.jpg is a valid image and should not be reported as broken");
        }
    }
}
