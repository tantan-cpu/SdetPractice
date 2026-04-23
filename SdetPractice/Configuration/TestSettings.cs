using Microsoft.Extensions.Configuration;

namespace SdetPractice.Configuration
{
    /// <summary>Singleton that holds test configuration values loaded from appsettings.json.</summary>
    public class TestSettings
    {
        private static TestSettings? _instance;
        private static readonly object _lock = new();

        /// <summary>Base URL for all page navigations.</summary>
        public string BaseUrl { get; init; } = string.Empty;

        /// <summary>Browser to use (Chrome, Firefox, Edge).</summary>
        public string Browser { get; init; } = "Chrome";

        /// <summary>Whether to run the browser in headless mode.</summary>
        public bool Headless { get; init; } = false;

        /// <summary>Timeout in seconds for explicit waits.</summary>
        public int ExplicitWaitSeconds { get; init; } = 10;

        /// <summary>Timeout in seconds for page load.</summary>
        public int PageLoadTimeoutSeconds { get; init; } = 30;

        /// <summary>Username for Basic Auth protected pages.</summary>
        public string BasicAuthUsername { get; init; } = string.Empty;

        /// <summary>Password for Basic Auth protected pages.</summary>
        public string BasicAuthPassword { get; init; } = string.Empty;

        /// <summary>Returns the single shared instance, loading it from config on first access.</summary>
        public static TestSettings Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ??= Load();
                }
            }
        }

        private static TestSettings Load()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();

            var settings = new TestSettings();
            config.GetSection("TestSettings").Bind(settings);
            return settings;
        }
    }
}