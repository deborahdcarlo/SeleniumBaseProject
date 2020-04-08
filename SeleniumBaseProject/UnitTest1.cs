using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace SeleniumBaseProject
{
    public class Tests
    {
        

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            
            var driver = GetDriver();
            driver.Navigate().GoToUrl("https://ultimateqa.com/simple-html-elements-for-automation");

            IWebElement radioButton = driver.FindElement(By.XPath("//*[@name='gender'][@value = 'female']"));
            radioButton.Click();

            IWebElement checkbox = driver.FindElement(By.XPath("//*[@type='checkbox'][@value = 'Car']"));
            checkbox.Click();

            IWebElement dropdown = driver.FindElement(By.XPath("//select/option[contains(text(),'Audi')]"));
            dropdown.Click();
   
            var salary = By.XPath("//*[@id='htmlTableId']//td[contains(text(), '$150,000+')]");
            HighlightElementUsingJavaScript(salary, driver);

            IWebElement tab2 = driver.FindElement(By.XPath("//*[@class='et_pb_tab_1']"));
            tab2.Click();

            Assert.AreEqual("Tab 2 content", driver.FindElement(By.XPath("//*[@class='et_pb_tab_1 et_pb_tab_active']")).Text);

            var location = By.XPath("//*[@class='et_pb_column et_pb_column_1_3 et_pb_column_10 et_pb_css_mix_blend_mode_passthrough']");

            HighlightElementUsingJavaScript(location, driver);
        }

        private IWebDriver GetDriver()
        {
            return new ChromeDriver();
        }

        private void HighlightElementUsingJavaScript(By locationStrategy, IWebDriver driver, int duration = 2)
        {
            var element = driver.FindElement(locationStrategy);
            var originalStyle = element.GetAttribute("style");
            IJavaScriptExecutor JavaScriptExecutor = driver as IJavaScriptExecutor;
            JavaScriptExecutor.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2])",
                element,
                "style",
                "border: 7px solid yellow; border-style: dashed;");

            if (duration <= 0) return;
            Thread.Sleep(TimeSpan.FromSeconds(duration));
            JavaScriptExecutor.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2])",
                element,
                "style",
                originalStyle);
        }
    }
}