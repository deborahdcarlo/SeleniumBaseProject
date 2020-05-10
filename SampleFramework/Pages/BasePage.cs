using OpenQA.Selenium;

namespace Pages.SampleFramework
{
    internal class BasePage
    {
        protected IWebDriver Driver { get; set; }

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
        }

    }
}