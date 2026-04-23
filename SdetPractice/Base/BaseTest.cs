using OpenQA.Selenium;
using Serilog;
using SdetPractice.Configuration;
using SdetPractice.Drivers;
using SdetPractice.Utilities;

namespace SdetPractice.Base
{
    /// <summary>Base class for all test fixtures. Manages driver lifecycle, logging, and failure screenshots.</summary>
    public abstract class BaseTest
    {
        /// <summary>The active WebDriver instance for the current test.</summary>
        protected IWebDriver Driver { get; private set; } = null!;

        /// <summary>Initialises the logger, creates the WebDriver, and sets timeouts before each test.</summary>
        [SetUp]
        public void SetUp()
        {
            TestLogger.Initialize();

            var settings = TestSettings.Instance;
            var browserType = Enum.Parse<BrowserType>(settings.Browser, ignoreCase: true);

            Driver = DriverFactory.CreateDriver(browserType);

            if (!settings.Headless)
                Driver.Manage().Window.Maximize();

            Driver.Manage().Timeouts().PageLoad =
                TimeSpan.FromSeconds(settings.PageLoadTimeoutSeconds);

            Log.Information("Test started: {TestName}",
                TestContext.CurrentContext.Test.Name);
        }

        /// <summary>Captures a screenshot on failure, quits the driver, and flushes the logger after each test.</summary>
        [TearDown]
        public void TearDown()
        {
            var testName = TestContext.CurrentContext.Test.Name;
            var outcome = TestContext.CurrentContext.Result.Outcome.Status;

            Log.Information("Test '{TestName}' finished with status: {Status}",
                testName, outcome);

            if (outcome == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                ScreenshotHelper.TakeScreenshot(Driver, testName);
            }

            Driver?.Quit();
            Driver?.Dispose();
            TestLogger.CloseAndFlush();
        }
    }
}