using OpenQA.Selenium;

namespace Pages.SampleFramework
{
    internal class UltimateQAHomePage : BasePage
    {
        public bool IsVisible => Driver.Title.Contains("Home - Ultimate QA");

        public UltimateQAHomePage(IWebDriver driver) : base(driver){}
    }
}