using OpenQA.Selenium;
using SdetPractice.Base;

namespace SdetPractice.Pages
{
    /// <summary>Page object for the Challenging DOM page (/challenging_dom). Demonstrates stable locator strategies on a dynamic page.</summary>
    public class ChallengingDomPage : BasePage
    {
        protected override By HeadingLocator => By.TagName("h3");

        // Buttons — using class selectors, NOT dynamic IDs
        private readonly By _defaultButton = By.CssSelector("a.button:not(.alert):not(.success)");
        private readonly By _alertButton   = By.CssSelector("a.button.alert");
        private readonly By _successButton = By.CssSelector("a.button.success");

        // Table
        private readonly By _tableHeaders = By.CssSelector("table thead th");
        private readonly By _tableRows    = By.CssSelector("table tbody tr");

        // Canvas
        private readonly By _canvas = By.Id("canvas");

        public ChallengingDomPage(IWebDriver driver) : base(driver) { }

        /// <inheritdoc/>
        public override void Open() => NavigateTo("/challenging_dom");

        /// <summary>Validates the page is loaded by confirming the heading is visible.</summary>
        public override bool IsLoaded() => IsVisible(HeadingLocator);

        /// <summary>Clicks the default (blue) button.</summary>
        public void ClickDefaultButton() => Click(_defaultButton);

        /// <summary>Clicks the alert (red) button.</summary>
        public void ClickAlertButton() => Click(_alertButton);

        /// <summary>Clicks the success (green) button.</summary>
        public void ClickSuccessButton() => Click(_successButton);

        /// <summary>Returns the text of all non-empty table column headers.</summary>
        public List<string> GetTableHeaders()
        {
            return Driver.FindElements(_tableHeaders)
                .Select(th => th.Text.Trim())
                .Where(text => !string.IsNullOrEmpty(text))
                .ToList();
        }

        /// <summary>Returns the number of rows in the table body.</summary>
        public int GetTableRowCount() => Driver.FindElements(_tableRows).Count;

        /// <summary>Returns true if the canvas element is present in the DOM.</summary>
        public bool IsCanvasPresent() => IsPresent(_canvas);

        /// <summary>Reads the answer value rendered on the canvas by extracting it from the inline script tag.</summary>
        public string? GetCanvasAnswer()
        {
            var js = (IJavaScriptExecutor)Driver;
            return js.ExecuteScript(@"
                var scripts = document.querySelectorAll('script');
                for (var s of scripts) {
                    var match = s.textContent.match(/Answer:\s*(\d+)/);
                    if (match) return match[1];
                }
                return null;
            ") as string;
        }
    }
}
