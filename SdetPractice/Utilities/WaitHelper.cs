using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SdetPractice.Utilities
{
    /// <summary>Wraps WebDriverWait with common explicit wait conditions.</summary>
    public class WaitHelper
    {
        private readonly WebDriverWait _wait;

        /// <summary>Initialises the wait helper. StaleElementReferenceException is ignored to handle DOM re-renders during waits.</summary>
        public WaitHelper(IWebDriver driver, int timeoutSeconds)
        {
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            _wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
        }

        /// <summary>Waits until the element is visible and returns it.</summary>
        public IWebElement WaitUntilVisible(By locator)
            => _wait.Until(ExpectedConditions.ElementIsVisible(locator));

        /// <summary>Waits until the element is clickable and returns it.</summary>
        public IWebElement WaitUntilClickable(By locator)
            => _wait.Until(ExpectedConditions.ElementToBeClickable(locator));

        /// <summary>Waits until the element is no longer visible and returns true.</summary>
        public bool WaitUntilInvisible(By locator)
            => _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
    }
}
