using Serilog;

namespace SdetPractice.Utilities
{
    /// <summary>Configures and manages the Serilog logger used across all tests.</summary>
    public static class TestLogger
    {
        /// <summary>Initialises the logger with console and rolling file sinks.</summary>
        public static void Initialize()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/test-.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        /// <summary>Flushes and closes the logger at the end of a test.</summary>
        public static void CloseAndFlush() => Log.CloseAndFlush();
    }
}
