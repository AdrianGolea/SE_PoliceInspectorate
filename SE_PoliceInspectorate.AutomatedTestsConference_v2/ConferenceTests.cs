using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace ConferenceMessageTests
{
    [TestClass]
    public class ConferenceMessageTests
    {
        private IWebDriver driver;

        [TestInitialize]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver.Quit();
            driver.Dispose();
        }

        [TestMethod]
        public void VerifyConferenceMessageIndexPage()
        {
            //driver.Navigate().GoToUrl("http://localhost:port/ConferenceMessage/Index");
            driver.Navigate().GoToUrl("http://localhost:port/ConferenceMessage/Index");
            string expectedTitle = "Conference Message Index";
            string actualTitle = driver.Title;

            Assert.AreEqual(expectedTitle, actualTitle);
        }

        //[TestMethod]
        //public void VerifyConferenceMessageDetailsPage()
        //{
        //    driver.Navigate().GoToUrl("http://localhost:port/ConferenceMessage/Conference/1");

        //    string expectedTitle = "Conference Details";
        //    string actualTitle = driver.Title;

        //    Assert.AreEqual(expectedTitle, actualTitle);

        //    var messages = driver.FindElements(By.ClassName("conference-message"));

        //    Assert.IsTrue(messages.Count > 0);
        //}

        [TestMethod]
        public void CreateConferenceMessage()
        {
            driver.Navigate().GoToUrl("https://localhost:7099/ConferenceMessage/Conference/1");

            string content = "Test Conference Message";

            IWebElement contentInput = driver.FindElement(By.Id("content"));
            contentInput.SendKeys(content);

            IWebElement submitButton = driver.FindElement(By.Id("submit"));
            submitButton.Click();

            var messages = driver.FindElements(By.ClassName("conference-message"));

            bool messageCreated = false;

            foreach (var message in messages)
            {
                if (message.Text.Contains(content))
                {
                    messageCreated = true;
                    break;
                }
            }

            Assert.IsTrue(messageCreated);
        }

        [TestMethod]
        public void VerifyUserListOnIndexPage()
        {
            driver.Navigate().GoToUrl("https://localhost:7099/ConferenceMessage");

            var userList = driver.FindElement(By.Id("user-list"));

            Assert.IsNotNull(userList);

            var users = driver.FindElements(By.ClassName("user"));

            Assert.IsTrue(users.Count > 0);
        }

        [TestMethod]
        public void VerifyCurrentUserId()
        {
            driver.Navigate().GoToUrl("https://localhost:7099/ConferenceMessage/Conference/1");

            var currentUserId = driver.FindElement(By.Id("current-user-id"));

            Assert.IsNotNull(currentUserId);

            string userId = currentUserId.Text;

            Assert.IsFalse(string.IsNullOrEmpty(userId));
        }
    }
}
