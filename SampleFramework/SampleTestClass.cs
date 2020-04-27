using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace SampleFramework
{
    [TestClass]
    public class SampleTestClass
    {
        private IWebDriver Driver { get; set; }

        [TestMethod]
        public void SampleTest()
        {
            Driver = GetDriver();

            var sampleApplicationPage = new SampleApplicationPage(Driver);
            sampleApplicationPage.GoTo();
            Assert.IsTrue(sampleApplicationPage.IsVisible, "Sample Application Page was not visible");

            var ultimateQAHomePage = sampleApplicationPage.FillOutFormAndSubmit("Deborah");
            Assert.IsTrue(ultimateQAHomePage.IsVisible, "Ultimate QA Page was not visible");
        }

        private IWebDriver GetDriver()
        {
            return new ChromeDriver();
        }

        [TestCleanup]
        public void TearDown()
        {
            Driver.Close();
            Driver.Quit();
        }
    }
}
