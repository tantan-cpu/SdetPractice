using FluentAssertions;
using SdetPractice.Base;
using SdetPractice.Pages;

namespace SdetPractice.Tests.UI
{
    /// <summary>Test suite for the Add/Remove Elements page.</summary>
    [TestFixture]
    public class AddOrRemoveElementsUITests : BaseTest
    {
        private AddOrRemoveElementsPage _page = null!;

        /// <summary>Opens the Add/Remove Elements page before each test.</summary>
        [SetUp]
        public void SetUpPage()
        {
            _page = new AddOrRemoveElementsPage(Driver);
            _page.Open();
        }

        [Test]
        [Description("TC002 - Validate Add/Remove Elements page loads successfully")]
        public void Page_ShouldLoadSuccessfully()
        {
            _page.IsLoaded()
                .Should().BeTrue("Page should load with main elements visible");
        }

        [Test]
        [Description("TC003 - Validate heading text is correct")]
        public void Heading_ShouldBeCorrect()
        {
            _page.GetHeadingText()
                .Should().Contain("Add/Remove Elements");
        }

        [Test]
        [Description("TC004 - Validate Delete button appears after clicking Add Element")]
        public void AddElement_ShouldShowDeleteButton()
        {
            _page.ClickAddElementButton();

            _page.IsDeleteButtonVisible()
                .Should().BeTrue("Delete button should appear after clicking Add");
        }

        [Test]
        [Description("TC005 - Validate Delete button disappears after clicking Delete Element")]
        public void DeleteElement_ShouldRemoveDeleteButton()
        {
            _page.ClickAddElementButton();
            _page.ClickDeleteElementButton();

            _page.WaitUntilDeleteButtonDisappears();

            _page.IsDeleteButtonGone()
                .Should().BeTrue("Delete button should be removed after deletion");
        }
    }
}