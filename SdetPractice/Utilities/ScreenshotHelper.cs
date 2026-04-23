    using OpenQA.Selenium;
    using Serilog;

    namespace SdetPractice.Utilities
    {
        /// <summary>Captures and saves screenshots to disk, typically on test failure.</summary>
        public static class ScreenshotHelper
        {
            private static readonly string ScreenshotsFolder =
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");

            /// <summary>Takes a screenshot and saves it to the Screenshots folder with a timestamped filename.</summary>
            public static void TakeScreenshot(IWebDriver driver, string testName)
            {
                try
                {
                    if (driver is not ITakesScreenshot screenshotDriver) return;

                    Directory.CreateDirectory(ScreenshotsFolder);

                    var fileName = $"{SanitizeName(testName)}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                    var filePath = Path.Combine(ScreenshotsFolder, fileName);

                    screenshotDriver.GetScreenshot().SaveAsFile(filePath);
                    Log.Information("Screenshot saved: {FilePath}", filePath);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Screenshot failed for test: {TestName}", testName);
                }
            }

            private static string SanitizeName(string name)
            {
                var invalid = Path.GetInvalidFileNameChars();
                var sanitized = string.Join("_", name.Split(invalid, StringSplitOptions.RemoveEmptyEntries));
                return sanitized.Length > 50 ? sanitized[..50] : sanitized;
            }
        }
    }
