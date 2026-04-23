using Allure.NUnit.Attributes;
using FluentAssertions;
using SdetPractice.Base;
using SdetPractice.Pages;

namespace SdetPractice.Tests.UI
{
    /// <summary>UI test suite for the Challenging DOM page. Demonstrates stable locator strategies on a dynamic page.</summary>
    [TestFixture]
    [AllureSuite("UI Tests")]
    [AllureFeature("Challenging DOM")]
    public class ChallengingDomUITests : BaseTest
    {
        private ChallengingDomPage _page = null!;

        /// <summary>Opens the Challenging DOM page before each test.</summary>
        [SetUp]
        public void SetUpPage()
        {
            _page = new ChallengingDomPage(Driver);
            _page.Open();
        }

        [Test]
        [Description("TC021 - Validate Challenging DOM page loads successfully")]
        public void ChallengingDomPage_ShouldLoadSuccessfully()
        {
            _page.IsLoaded()
                .Should().BeTrue("page should load with the heading visible");
        }

        [Test]
        [Description("TC022 - Validate table headers contain expected column names")]
        public void ChallengingDomPage_ShouldHaveCorrectTableHeaders()
        {
            var headers = _page.GetTableHeaders();

            headers.Should().Contain("Ipsum",   "Ipsum column should be present");
            headers.Should().Contain("Dolor",   "Dolor column should be present");
            headers.Should().Contain("Sit",     "Sit column should be present");
            headers.Should().Contain("Amet",    "Amet column should be present");
            headers.Should().Contain("Diceret", "Diceret column should be present");
            headers.Should().Contain("Action",  "Action column should be present");
        }

        [Test]
        [Description("TC023 - Validate table row count is correct")]
        public void ChallengingDomPage_ShouldHaveCorrectTableRowCount()
        {
            _page.GetTableRowCount()
                .Should().BeGreaterThan(0, "table should contain at least one row");
        }

        [Test]
        [Description("TC024 - Validate canvas element is present on the page")]
        public void ChallengingDomPage_CanvasElement_ShouldBePresent()
        {
            _page.IsCanvasPresent()
                .Should().BeTrue("canvas element should be present on the page");
        }

        [Test]
        [Description("TC025 - Validate canvas renders a numeric answer via JavaScript")]
        public void ChallengingDomPage_Canvas_ShouldRenderNumericAnswer()
        {
            var answer = _page.GetCanvasAnswer();

            answer.Should().NotBeNullOrEmpty("canvas should render an answer value");

            int.TryParse(answer, out var number).Should().BeTrue("canvas answer should be a valid number");

            number.Should().BeGreaterThan(0, "canvas answer should be a positive number");
        }

        [Test]
        [Description("TC026 - Validate default button is clickable")]
        public void ChallengingDomPage_DefaultButton_ShouldBeClickable()
        {
            var action = () => _page.ClickDefaultButton();

            action.Should().NotThrow("default button should be clickable without errors");
        }

        [Test]
        [Description("TC027 - Validate alert button is clickable")]
        public void ChallengingDomPage_AlertButton_ShouldBeClickable()
        {
            var action = () => _page.ClickAlertButton();

            action.Should().NotThrow("alert button should be clickable without errors");
        }

        [Test]
        [Description("TC028 - Validate success button is clickable")]
        public void ChallengingDomPage_SuccessButton_ShouldBeClickable()
        {
            var action = () => _page.ClickSuccessButton();

            action.Should().NotThrow("success button should be clickable without errors");
        }
    }
}
