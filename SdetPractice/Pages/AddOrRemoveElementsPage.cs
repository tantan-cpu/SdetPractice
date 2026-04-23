using OpenQA.Selenium;
using SdetPractice.Base;

namespace SdetPractice.Pages
{
    /// <summary>Page object for the Add/Remove Elements page (/add_remove_elements/).</summary>
    public class AddOrRemoveElementsPage : BasePage
    {
        protected override By HeadingLocator => By.TagName("h3");
        private readonly By _addButton = By.CssSelector(".example button");
        private readonly By _deleteButton = By.CssSelector("#elements .added-manually");

        public AddOrRemoveElementsPage(IWebDriver driver) : base(driver) { }

        /// <inheritdoc/>
        public override void Open() => NavigateTo("/add_remove_elements/");

        /// <summary>Validates the page is loaded by confirming the heading and Add Element button are visible.</summary>
        public override bool IsLoaded() => IsVisible(HeadingLocator) && IsVisible(_addButton);

        /// <summary>Performs a click on the Add Element button, which dynamically adds a Delete button to the UI.</summary>
        public void ClickAddElementButton() => Click(_addButton);

        /// <summary>Performs a click on the Delete button, which removes it from the UI.</summary>
        public void ClickDeleteElementButton() => Click(_deleteButton);

        /// <summary>Waits until the Delete button disappears — use before asserting element removal.</summary>
        public void WaitUntilDeleteButtonDisappears() => WaitForElementToDisappear(_deleteButton);

        /// <summary>Returns true if the Delete button is visible. Use to assert it appeared after clicking Add.</summary>
        public bool IsDeleteButtonVisible() => IsVisible(_deleteButton);

        /// <summary>Returns true if the Delete button is fully removed from the DOM. Use to assert clean deletion.</summary>
        public bool IsDeleteButtonGone() => !IsPresent(_deleteButton);
    }
}