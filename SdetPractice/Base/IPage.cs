namespace SdetPractice.Base
{
    /// <summary>Contract that all page objects must implement.</summary>
    public interface IPage
    {
        /// <summary>Navigates to the page URL.</summary>
        void Open();

        /// <summary>Returns true if the page loaded successfully.</summary>
        bool IsLoaded();

        /// <summary>Returns the main heading text of the page.</summary>
        string GetHeadingText();
    }
}
