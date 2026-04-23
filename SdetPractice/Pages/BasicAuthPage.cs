using OpenQA.Selenium;
using SdetPractice.Base;
using SdetPractice.Configuration;

namespace SdetPractice.Pages
{
    /// <summary>Page object for the Basic Auth page. Handles credential injection via URL to bypass the browser auth popup.</summary>
    public class BasicAuthPage : BasePage
    {
        protected override By HeadingLocator => By.TagName("h3");
        private readonly By _successMessage = By.CssSelector(".example p");

        public BasicAuthPage(IWebDriver driver) : base(driver) { }

        /// <summary>Navigates to the Basic Auth page with credentials embedded in the URL.</summary>
        public override void Open()
        {
            var settings = TestSettings.Instance;
            var uri = new Uri(settings.BaseUrl);
            var credentialUrl = $"{uri.Scheme}://{settings.BasicAuthUsername}:{settings.BasicAuthPassword}@{uri.Host}/basic_auth";
            Driver.Navigate().GoToUrl(credentialUrl);
        }

        /// <summary>Validates the page is loaded by confirming the heading is visible.</summary>
        public override bool IsLoaded() => IsVisible(HeadingLocator);

        /// <summary>Returns the success message text displayed after successful authentication.</summary>
        public string GetSuccessMessage() => GetText(_successMessage);
    }
}
