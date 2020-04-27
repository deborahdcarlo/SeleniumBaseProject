using OpenQA.Selenium;
using System;

namespace SampleFramework
{
    internal class SampleApplicationPage : BasePage
    {
        public bool IsVisible => Driver.Title.Contains("Sample Application Lifecycle - Sprint 1 - Ultimate QA");

        public IWebElement FirstNameField => Driver.FindElement(By.Name("firstname"));

        public IWebElement SubmitButton => Driver.FindElement(By.Id("submitForm"));

        public SampleApplicationPage(IWebDriver driver) : base(driver) {}

        internal void GoTo()
        {
            Driver.Navigate().GoToUrl("https://ultimateqa.com/sample-application-lifecycle-sprint-1");
        }

        public UltimateQAHomePage FillOutFormAndSubmit(string name)
        {
            FirstNameField.SendKeys(name);
            SubmitButton.Click();

            return new UltimateQAHomePage(Driver);
        }
    }
}