using OpenQA.Selenium;
using System;

namespace SampleFramework
{
    internal class SampleApplicationPage : BasePage
    {
        public bool IsVisible => Driver.Title.Contains("Sample Application Lifecycle - Sprint 3");

        public IWebElement FirstNameField => Driver.FindElement(By.Name("firstname"));

        public IWebElement SubmitButton => Driver.FindElement(By.XPath("//*[@type='submit']"));

        public IWebElement LastNameField => Driver.FindElement(By.Name("lastname"));

        public IWebElement MaleRadioButton => Driver.FindElement(By.XPath("//*[@type='radio' and @value='male']"));
        public IWebElement FemaleRadioButton => Driver.FindElement(By.XPath("//*[@type='radio' and @value='female']"));
        public IWebElement OtherRadioButton => Driver.FindElement(By.XPath("//*[@type='radio' and @value='other']"));

        public SampleApplicationPage(IWebDriver driver) : base(driver) {}

        internal void GoTo()
        {
            Driver.Navigate().GoToUrl("https://ultimateqa.com/sample-application-lifecycle-sprint-3");
        }

        public UltimateQAHomePage FillOutFormAndSubmit(TestUser user)
        {
            SetGender(user);

            FirstNameField.SendKeys(user.FirstName);
            LastNameField.SendKeys(user.LastName);
            SubmitButton.Click();

            return new UltimateQAHomePage(Driver);
        }

        private void SetGender(TestUser user)
        {
            switch (user.Gender)
            {
                case Gender.Female:
                    FemaleRadioButton.Click();
                    break;
                case Gender.Male:
                    MaleRadioButton.Click();
                    break;
                case Gender.Other:
                    OtherRadioButton.Click();
                    break;
                default:
                    break;
            }
        }
    }
}