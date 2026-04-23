using OpenQA.Selenium;
using Serilog;
using SdetPractice.Configuration;
using SdetPractice.Utilities;

namespace SdetPractice.Base
{
    /// <summary>Base class for all Page Object Model (POM) classes. Provides reusable UI interaction and synchronization helpers for test automation.</summary>
    public abstract class BasePage : IPage
    {
        /// <summary>The active WebDriver instance.</summary>
        protected readonly IWebDriver Driver;

        /// <summary>Explicit wait helper configured from test settings.</summary>
        protected readonly WaitHelper Wait;

        private readonly string _baseUrl;

        /// <summary>Initialises the page with a driver and reads configuration from TestSettings.</summary>
        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
            _baseUrl = TestSettings.Instance.BaseUrl;
            Wait = new WaitHelper(driver, TestSettings.Instance.ExplicitWaitSeconds);
        }

        /// <summary>Override to provide the locator for the page heading. Used by <see cref="GetHeadingText"/> and load validation.</summary>
        protected virtual By? HeadingLocator => null;

        /// <inheritdoc/>
        public abstract bool IsLoaded();

        /// <inheritdoc/>
        public abstract void Open();

        /// <inheritdoc/>
        public string GetHeadingText()
        {
            if (HeadingLocator == null)
                throw new NotSupportedException("This page has no heading defined.");

            return GetText(HeadingLocator);
        }

        /// <summary>Navigates to the given relative path appended to the base URL.</summary>
        protected void NavigateTo(string path)
        {
            var url = $"{_baseUrl}{path}";
            Log.Information("Navigating to {Url}", url);
            Driver.Navigate().GoToUrl(url);
        }

        /// <summary>Waits for the element to become visible and returns it.</summary>
        protected IWebElement WaitForElement(By locator)
            => Wait.WaitUntilVisible(locator);

        /// <summary>Waits for the element to be clickable and clicks it.</summary>
        protected void Click(By locator)
        {
            Log.Debug("Clicking element: {Locator}", locator);
            Wait.WaitUntilClickable(locator).Click();
        }

        /// <summary>Clears the element and types the given text into it.</summary>
        protected void Type(By locator, string text)
        {
            Log.Debug("Typing '{Text}' into element: {Locator}", text, locator);

            var element = Wait.WaitUntilVisible(locator); // more stable than clickable
            element.Clear();
            element.SendKeys(text);
        }

        /// <summary>Returns the visible text of the element matched by the locator.</summary>
        protected string GetText(By locator)
        {
            var text = Wait.WaitUntilVisible(locator).Text;
            Log.Debug("Got text '{Text}' from element: {Locator}", text, locator);
            return text;
        }

        /// <summary>Returns true if the element exists in the DOM regardless of visibility. Use for removed-element assertions.</summary>
        protected bool IsPresent(By locator)
        {
            return Driver.FindElements(locator).Count > 0;
        }

        /// <summary>Returns true if the element is visible within the wait timeout; false on timeout. Use for conditional UI assertions.</summary>
        protected bool IsVisible(By locator)
        {
            try
            {
                return Wait.WaitUntilVisible(locator) != null;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        /// <summary>Waits until the element is no longer visible and returns true when it disappears.</summary>
        protected bool WaitForElementToDisappear(By locator)
            => Wait.WaitUntilInvisible(locator);

        /// <summary>Returns the trimmed text content of a DOM node relative to the given element using a JS traversal expression (e.g. nextSibling, previousSibling, parentElement).</summary>
        protected string GetRelativeNodeText(IWebElement element, string nodeExpression = "nextSibling")
        {
            return ((IJavaScriptExecutor)Driver)
                .ExecuteScript($"return arguments[0].{nodeExpression}.textContent.trim();", element)
                ?.ToString() ?? string.Empty;
        }
    }
}