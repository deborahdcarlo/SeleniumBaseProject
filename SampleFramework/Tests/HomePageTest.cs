using AutomationResources;
using Enums.SampleFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.SampleFramework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Pages.SampleFramework;
using System;
using System.ComponentModel;
using DescriptionAttribute = Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute;

namespace Tests.SampleFramework
{
    [TestClass]
    [TestCategory("HomePage")]
    public class HomePageTest : BaseTest
    {
        
        internal HomePage HomePage { get; private set; }

        [TestMethod]
        [Description("This tests the form submition in the home page")]
        [DataRow("Deborah", "Oliveira", Gender.Female, DisplayName = "Female Test")]
        [DataRow("Frederico", "Pinto", Gender.Male, DisplayName = "Male Test")]
        [DataRow("Faísco", "Rodrigues", Gender.Other, DisplayName = "Other Test")]
        public void SampleTest(string firstName, string lastName, Gender gender)
        {

            HomePage = new HomePage(Driver);
            var testUser = CreateUser(firstName, lastName, gender);
            var emergencyContactUser = CreateUser("emergency", "test", gender);

            HomePage.GoTo();
            Assert.IsTrue(HomePage.IsVisible, "Sample Application Page was not visible");

            HomePage.FillOutPersonalDetails(testUser);            
            HomePage.FillOutEmergencyContact(emergencyContactUser);
            var ultimateQAHomePage = HomePage.SubmitForm();

            Assert.IsTrue(ultimateQAHomePage.IsVisible, "Ultimate QA Page was not visible");
        }

        private TestUser CreateUser(string firstName, string lastName, Gender gender)
        {
            return new TestUser
            {
                FirstName = firstName,
                LastName = lastName,
                Gender = gender
            };
        }
    }
}
