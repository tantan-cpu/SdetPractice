using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using SdetPractice.Configuration;

namespace SdetPractice.Drivers
{
    /// <summary>Supported browser types for WebDriver creation.</summary>
    public enum BrowserType
    {
        Chrome,
        Firefox,
        Edge
    }

    /// <summary>Factory that creates a configured WebDriver instance based on the requested browser type.</summary>
    public static class DriverFactory
    {
        /// <summary>Creates and returns a WebDriver for the specified browser type.</summary>
        public static IWebDriver CreateDriver(BrowserType browserType)
        {
            return browserType switch
            {
                BrowserType.Chrome => CreateChromeDriver(),
                BrowserType.Firefox => CreateFirefoxDriver(),
                BrowserType.Edge => CreateEdgeDriver(),
                _ => throw new ArgumentException($"Browser type '{browserType}' is not supported.")
            };
        }

        private static void ApplyChromiumHeadlessArgs<T>(T options)
            where T : ChromiumOptions
        {
            options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1920,1080");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
        }

        private static IWebDriver CreateChromeDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");

            if (TestSettings.Instance.Headless)
                ApplyChromiumHeadlessArgs(options);

            return new ChromeDriver(options);
        }

        private static IWebDriver CreateFirefoxDriver()
        {
            var options = new FirefoxOptions();
            options.AddArgument("--disable-notifications");

            if (TestSettings.Instance.Headless)
                options.AddArgument("--headless");

            return new FirefoxDriver(options);
        }

        private static IWebDriver CreateEdgeDriver()
        {
            var options = new EdgeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");

            if (TestSettings.Instance.Headless)
                ApplyChromiumHeadlessArgs(options);

            return new EdgeDriver(options);
        }
    }
}