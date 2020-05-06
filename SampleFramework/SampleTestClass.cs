using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.ComponentModel;

namespace SampleFramework
{
    [TestClass]
    public class SampleTestClass
    {
        private IWebDriver Driver { get; set; }
        internal SampleApplicationPage SampleApplicationPage { get; private set; }

        [TestInitialize]
        public void TestSetUp()
        {
            Driver = GetDriver();
            SampleApplicationPage = new SampleApplicationPage(Driver);
        }

        [TestMethod]
        [DataRow("Deborah", "Oliveira", Gender.Female, DisplayName = "Female Test")]
        [DataRow("Frederico", "Pinto", Gender.Male, DisplayName = "Male Test")]
        [DataRow("Faísco", "Rodrigues", Gender.Other, DisplayName = "Other Test")]
        public void SampleTest(string firstName, string lastName, Gender gender)
        {
            var testUser = CreateUser(firstName, lastName, gender);

            SampleApplicationPage.GoTo();
            Assert.IsTrue(SampleApplicationPage.IsVisible, "Sample Application Page was not visible");

            var ultimateQAHomePage = SampleApplicationPage.FillOutFormAndSubmit(testUser);
            Assert.IsTrue(ultimateQAHomePage.IsVisible, "Ultimate QA Page was not visible");
        }


        [TestCleanup]
        public void TestCleanUp()
        {
            Driver.Close();
            Driver.Quit();
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

        private IWebDriver GetDriver()
        {
            return new ChromeDriver();
        }
    }
}
