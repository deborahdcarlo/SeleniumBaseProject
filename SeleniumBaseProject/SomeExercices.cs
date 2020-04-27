using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Data;
using System.Threading;

namespace SeleniumBaseProject
{
    public class Tests
    {
        private IWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            _driver = GetDriver();
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Close();
            _driver.Quit();
        }

        [Test]
        public void Test1()
        {
            _driver.Navigate().GoToUrl("https://ultimateqa.com/simple-html-elements-for-automation");

            IWebElement radioButton = _driver.FindElement(By.XPath("//*[@name='gender'][@value = 'female']"));
            radioButton.Click();

            IWebElement checkbox = _driver.FindElement(By.XPath("//*[@type='checkbox'][@value = 'Car']"));
            checkbox.Click();

            IWebElement dropdown = _driver.FindElement(By.XPath("//select/option[contains(text(),'Audi')]"));
            dropdown.Click();

            var salary = By.XPath("//*[@id='htmlTableId']//td[contains(text(), '$150,000+')]");
            HighlightElementUsingJavaScript(salary, _driver);

            IWebElement tab2 = _driver.FindElement(By.XPath("//*[@class='et_pb_tab_1']"));
            tab2.Click();

            Assert.AreEqual("Tab 2 content", _driver.FindElement(By.XPath("//*[@class='et_pb_tab_1 et_pb_tab_active']")).Text);

            var location = By.XPath("//*[@class='et_pb_column et_pb_column_1_3 et_pb_column_10 et_pb_css_mix_blend_mode_passthrough']");

            HighlightElementUsingJavaScript(location, _driver);
        }

        [Test]
        [Category("Navigation")]
        public void Test2()
        {
            _driver.Navigate().GoToUrl("https://ultimateqa.com/");
            Assert.AreEqual("Home - Ultimate QA", _driver.Title);

            _driver.Navigate().GoToUrl("https://ultimateqa.com/automation");
            Assert.AreEqual("Automation Practice - Ultimate QA", _driver.Title);

            IWebElement hrefLocator = _driver.FindElement(By.XPath("//*[@href='../complicated-page']"));
            hrefLocator.Click();
            Assert.AreEqual("Complicated Page - Ultimate QA", _driver.Title);

            _driver.Navigate().Back();
            Assert.AreEqual("Automation Practice - Ultimate QA", _driver.Title);

        }

        [Test]
        [Category("Manipulation")]
        public void Test3()
        {
            _driver.Navigate().GoToUrl("https://ultimateqa.com/filling-out-forms");

            var nameField = _driver.FindElement(By.Id("et_pb_contact_name_1"));
            nameField.Clear();
            nameField.SendKeys("Deborah");

            var textField = _driver.FindElement(By.Id("et_pb_contact_message_1"));
            textField.Clear();
            textField.SendKeys("Hi");

            //var captcha = driver.FindElement(By.XPath("//*[@class='et_pb_contact_captcha_question']/following-sibling::input/@data-first_digit"));
            var captcha = _driver.FindElement(By.XPath("//*[@class='et_pb_contact_captcha_question']")).Text;
            captcha = captcha.Replace(" ", "");
            var numbers = captcha.Split('+');

            //another way
            //var table = new DataTable();
            //var captchaAnswer = (int)table.Compute(captcha, "");

            //another way

            //var value1 = driver.FindElement(By.XPath("//*[@class='et_pb_contact_captcha_question']/following-sibling::input")).GetAttribute("data-first_digit");


            var total = Convert.ToInt32(numbers[0]) + Convert.ToInt32(numbers[1]);

            var totalField = _driver.FindElement(By.XPath("//*[@class='et_pb_contact_captcha_question']/following-sibling::input"));
            totalField.Clear();
            totalField.SendKeys(total.ToString());

            var submit = _driver.FindElements(By.Name("et_builder_submit_button"));
            submit[1].Click();

            var successMessage = _driver.FindElement(By.ClassName("et-pb-contact-message")).FindElement(By.TagName("p"));
            Assert.IsTrue(successMessage.Text.Equals("Success"));
        }

        [Test]
        [Category("Element Interrogation")]
        public void Test4()
        {
            _driver.Navigate().GoToUrl("https://ultimateqa.com/simple-html-elements-for-automation");

            var button = _driver.FindElement(By.Id("button1"));
            Assert.AreEqual("submit", button.GetAttribute("type"));
            Assert.AreEqual("normal", button.GetCssValue("letter-spacing"));
            Assert.IsTrue(button.Displayed);
            Assert.IsTrue(button.Enabled);
            Assert.IsFalse(button.Selected);
            Assert.AreEqual("Click Me!", button.Text);
            Assert.AreEqual("button", button.TagName);
            Assert.AreEqual(21, button.Size.Height);
            Assert.AreEqual(341, button.Location.X);
            Assert.AreEqual(213, button.Location.Y);

        }

        [Test]
        [Category("Drag and Drop")]
        public void Test5()
        {
            _driver.Navigate().GoToUrl("http://www.pureexample.com/jquery-ui/basic-droppable.html");

            //var source = _driver.FindElement(By.XPath("//*[@class='square ui-draggable']"));
            var source = _driver.FindElements(By.XPath("//*[contains(text(), 'Drage me')]"));
            //var target = _driver.FindElement(By.XPath("//*[@class='squaredotted ui-droppable']"));
            var target = _driver.FindElements(By.XPath("//*[contains(text(), 'Drop here')]"));

            var action = new Actions(_driver);
            action.DragAndDrop(source[1], target[1]).Perform();

            var result = _driver.FindElement(By.Id("info"));
            Assert.AreEqual("dropped!", result.Text);
        }


        [Test]
        [Category("Drag and Drop")]
        public void Test6()
        {
            _driver.Navigate().GoToUrl("http://the-internet.herokuapp.com/drag_and_drop");

            var source = _driver.FindElement(By.Id("column-a"));
            var target = _driver.FindElement(By.Id("column-b"));

            var action = new Actions(_driver);
            //action.DragAndDrop(source, target).Perform();

            action
                .ClickAndHold(source)
                .MoveToElement(target)
                .Release(target)
                .Perform();

            var result = _driver.FindElement(By.XPath("//*[@id='column-a']/header"));
            Assert.AreEqual("B", result.Text);
        }

        [Test]
        public void Test7()
        {
            _driver.Navigate().GoToUrl("https://ultimateqa.com/");

            var searchIcon = By.Id("et_top_search");
            var searchElement = ElementIsDisplayed(searchIcon, _driver);

            searchElement.Click();
            var searchBox = _driver.FindElement(By.XPath("//*[@class='et-search-field']"));
            searchBox.SendKeys("Complicated Page");
            searchBox.SendKeys(Keys.Enter);

            var complicatedPage = By.XPath("//*[@href='https://ultimateqa.com/complicated-page/']");
            var complicatedPageElement = ElementIsDisplayed(complicatedPage, _driver);
            complicatedPageElement.Click();

            Assert.IsNotNull(ElementIsDisplayed(By.Id("Skills_Improved"), _driver));

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

        public IWebElement ElementIsDisplayed(By element, IWebDriver _driver)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, new TimeSpan(0,0,10));
                wait.Until(drv => element);
            }
            catch (WebDriverTimeoutException)
            {
                return null;
            }
            return _driver.FindElement(element);
        }

        /*Notes
         *//*[@id='id' and @style] -> to check if the property style is present inside the element with id 'id'
         */
    }
}