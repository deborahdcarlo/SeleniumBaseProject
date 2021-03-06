﻿using Enums.SampleFramework;
using Model.SampleFramework;
using NLog;
using OpenQA.Selenium;

namespace Pages.SampleFramework
{
    internal class HomePage : BasePage
    {
        public bool IsVisible => Driver.Title.Contains("Sample Application Lifecycle - Sprint 4");
        public IWebElement FirstNameField => Driver.FindElement(By.Name("firstname"));        
        public IWebElement LastNameField => Driver.FindElement(By.Name("lastname"));
        public IWebElement FirstNameEmergencyField => Driver.FindElement(By.Id("f2"));
        public IWebElement LastNameEmergencyField => Driver.FindElement(By.Id("l2"));
        public IWebElement SubmitButton => Driver.FindElement(By.XPath("//*[@type='submit']"));
        public IWebElement MaleRadioButton => Driver.FindElement(By.XPath("//*[@type='radio' and @value='male']"));
        public IWebElement FemaleRadioButton => Driver.FindElement(By.XPath("//*[@type='radio' and @value='female']"));
        public IWebElement OtherRadioButton => Driver.FindElement(By.XPath("//*[@type='radio' and @value='other']"));
        public IWebElement MaleEmergencyRadioButton => Driver.FindElement(By.Id("radio2-m"));
        public IWebElement FemaleEmergencyRadioButton => Driver.FindElement(By.Id("radio2-f"));
        public IWebElement OtherEmergencyRadioButton => Driver.FindElement(By.Id("radio2-0"));

        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public HomePage(IWebDriver driver) : base(driver) {}

        internal void GoTo()
        {
            var url = "https://ultimateqa.com/sample-application-lifecycle-sprint-4";
            Driver.Navigate().GoToUrl(url);            
            _logger.Info($"Opened url=> {url}");
        }

        public void FillOutPersonalDetails(TestUser user)
        {
            SetPersonalGender(user);
            FirstNameField.SendKeys(user.FirstName);
            LastNameField.SendKeys(user.LastName);
            _logger.Info($"Fill out Personal Contact Form with =>{user.FirstName} and {user.LastName}");

        }
        public void FillOutEmergencyContact(TestUser user)
        {
            SetEmergencyGender(user);
            FirstNameEmergencyField.SendKeys(user.FirstName);
            LastNameEmergencyField.SendKeys(user.LastName);
            _logger.Info($"Fill out Emergency Contact Form with =>{user.FirstName} and {user.LastName}");
        }

        public UltimateQAHomePage SubmitForm()
        {
            SubmitButton.Click();
            return new UltimateQAHomePage(Driver);
        }       

        private void SetPersonalGender(TestUser user)
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

        private void SetEmergencyGender(TestUser user)
        {
            switch (user.Gender)
            {
                case Gender.Female:
                    FemaleEmergencyRadioButton.Click();
                    break;
                case Gender.Male:
                    MaleEmergencyRadioButton.Click();
                    break;
                case Gender.Other:
                    OtherEmergencyRadioButton.Click();
                    break;
                default:
                    break;
            }
        }
    }
}