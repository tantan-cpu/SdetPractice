using OpenQA.Selenium;
using SdetPractice.Base;

namespace SdetPractice.Pages
{
    /// <summary>Page object for the Broken Images page (/broken_images).</summary>
    public class BrokenImagesPage : BasePage
    {
        protected override By HeadingLocator => By.TagName("h3");
        private readonly By _images = By.CssSelector(".example img");

        public BrokenImagesPage(IWebDriver driver) : base(driver) { }

        /// <inheritdoc/>
        public override void Open() => NavigateTo("/broken_images");

        /// <summary>Validates the page is loaded by confirming the heading is visible.</summary>
        public override bool IsLoaded() => IsVisible(HeadingLocator);

        /// <summary>Returns the src attribute of all images on the page.</summary>
        public List<string> GetAllImageSources()
        {
            return Driver.FindElements(_images)
                .Where(img => img.GetAttribute("src") != null)
                .Select(img => img.GetAttribute("src")!)
                .ToList();
        }

        /// <summary>Returns the src of images that failed to load, detected via JavaScript naturalWidth.</summary>
        public List<string> GetBrokenImageSources()
        {
            var js = (IJavaScriptExecutor)Driver;

            return Driver.FindElements(_images)
                .Where(img => (long)(js.ExecuteScript("return arguments[0].naturalWidth;", img) ?? 0L) == 0)
                .Where(img => img.GetAttribute("src") != null)
                .Select(img => img.GetAttribute("src")!)
                .ToList();
        }

        /// <summary>Returns the total number of images on the page.</summary>
        public int GetTotalImageCount() => Driver.FindElements(_images).Count;

        /// <summary>Returns the number of broken images on the page.</summary>
        public int GetBrokenImageCount() => GetBrokenImageSources().Count;
    }
}
