using OpenQA.Selenium;
using SdetPractice.Base;

namespace SdetPractice.Pages
{
    /// <summary>Page object for the AB Testing page (/abtest).</summary>
    public class ABTestingPage : BasePage
    {
        protected override By HeadingLocator => By.TagName("h3");
        private readonly By _description = By.TagName("p");

        public ABTestingPage(IWebDriver driver) : base(driver) { }

        /// <inheritdoc/>
        public override void Open() => NavigateTo("/abtest");

        /// <summary>Validates the page is loaded by confirming the h3 heading is visible.</summary>
        public override bool IsLoaded() => IsVisible(HeadingLocator);

        /// <summary>Returns the description paragraph text. Used to validate page content in tests.</summary>
        public string GetDescription() => GetText(_description);
    }
}