using FluentAssertions;
using SdetPractice.Base;
using SdetPractice.Pages;

namespace SdetPractice.Tests.UI
{
    /// <summary>UI test suite for the Checkboxes page. Validates checkbox default states and interaction behavior.</summary>
    [TestFixture]
    public class CheckboxesUITests : BaseTest
    {
        private CheckboxesPage _page = null!;

        /// <summary>Opens the Checkboxes page before each test.</summary>
        [SetUp]
        public void SetUpPage()
        {
            _page = new CheckboxesPage(Driver);
            _page.Open();
        }

        [Test]
        [Description("TC029 - Validate Checkboxes page loads successfully")]
        public void CheckboxesPage_ShouldLoadSuccessfully()
        {
            _page.IsLoaded()
                .Should().BeTrue("page should load with the heading visible");
        }

        [Test]
        [Description("TC030 - Validate default state of all checkboxes")]
        public void CheckboxesPage_ShouldHaveCorrectDefaultStates()
        {
            var checkboxes = _page.GetAllCheckboxes();

            checkboxes.Should().HaveCount(2, "page should have exactly 2 checkboxes");

            checkboxes[0].IsChecked.Should().BeFalse("checkbox 1 should be unchecked by default");
            checkboxes[1].IsChecked.Should().BeTrue("checkbox 2 should be checked by default");
        }

        [Test]
        [Description("TC031 - Validate checkbox 1 can be checked")]
        public void CheckboxesPage_Checkbox1_ShouldBeCheckable()
        {
            _page.Check("checkbox 1");

            _page.IsChecked("checkbox 1")
                .Should().BeTrue("checkbox 1 should be checked after clicking it");
        }

        [Test]
        [Description("TC032 - Validate checkbox 2 can be unchecked")]
        public void CheckboxesPage_Checkbox2_ShouldBeUncheckable()
        {
            _page.Uncheck("checkbox 2");

            _page.IsChecked("checkbox 2")
                .Should().BeFalse("checkbox 2 should be unchecked after clicking it");
        }

        [Test]
        [Description("TC033 - Validate checkbox 1 returns to unchecked after being checked then unchecked")]
        public void CheckboxesPage_Checkbox1_ShouldReturnToOriginalState()
        {
            _page.Check("checkbox 1");
            _page.Uncheck("checkbox 1");

            _page.IsChecked("checkbox 1")
                .Should().BeFalse("checkbox 1 should return to unchecked state");
        }

        [Test]
        [Description("TC034 - Validate checkbox 2 returns to checked after being unchecked then rechecked")]
        public void CheckboxesPage_Checkbox2_ShouldReturnToOriginalState()
        {
            _page.Uncheck("checkbox 2");
            _page.Check("checkbox 2");

            _page.IsChecked("checkbox 2")
                .Should().BeTrue("checkbox 2 should return to checked state");
        }
    }
}
