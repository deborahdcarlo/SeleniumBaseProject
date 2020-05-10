using AutomationResources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Tests.SampleFramework
{
    public class BaseTest
    {
        public IWebDriver Driver { get; set; }

        [TestInitialize]
        public void TestSetUp()
        {
            var factory = new WebDriverFactory();
            Driver = factory.Create(BrowserType.Chrome);
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            Driver.Close();
            Driver.Quit();
        }
    }
}