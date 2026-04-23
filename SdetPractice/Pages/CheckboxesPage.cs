using OpenQA.Selenium;
using SdetPractice.Base;

namespace SdetPractice.Pages
{
    /// <summary>Page object for the Checkboxes page (/checkboxes/).</summary>
    public class CheckboxesPage : BasePage
    {
        protected override By HeadingLocator => By.TagName("h3");
        private readonly By _checkboxes = By.CssSelector("#checkboxes input[type='checkbox']");

        public CheckboxesPage(IWebDriver driver) : base(driver) { }

        /// <inheritdoc/>
        public override void Open() => NavigateTo("/checkboxes");

        /// <summary>Validates the page is loaded by confirming the heading is visible.</summary>
        public override bool IsLoaded() => IsVisible(HeadingLocator);

        /// <summary>Returns all checkboxes as (Text, IsChecked) tuples.</summary>
        public List<(string Text, bool IsChecked)> GetAllCheckboxes()
        {
            return Driver.FindElements(_checkboxes)
                .Select(cb => (GetRelativeNodeText(cb), cb.Selected))
                .ToList();
        }

        /// <summary>Checks the checkbox with the given label text if not already checked.</summary>
        public void Check(string labelText)
        {
            var checkbox = Driver.FindElements(_checkboxes)
                .FirstOrDefault(cb => GetRelativeNodeText(cb) == labelText);

            if (checkbox != null && !checkbox.Selected)
                checkbox.Click();
        }

        /// <summary>Unchecks the checkbox with the given label text if not already unchecked.</summary>
        public void Uncheck(string labelText)
        {
            var checkbox = Driver.FindElements(_checkboxes)
                .FirstOrDefault(cb => GetRelativeNodeText(cb) == labelText);

            if (checkbox != null && checkbox.Selected)
                checkbox.Click();
        }

        /// <summary>Returns true if the checkbox with the given label text is checked.</summary>
        public bool IsChecked(string labelText)
        {
            return Driver.FindElements(_checkboxes)
                .Where(cb => GetRelativeNodeText(cb) == labelText)
                .Select(cb => cb.Selected)
                .FirstOrDefault();
        }

    }
}
