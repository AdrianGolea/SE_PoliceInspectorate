using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

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
            driver.Navigate().GoToUrl("http://localhost:port/ConferenceMessage/Index");
        }

        [TestMethod]
        public void VerifyIndexPageTitle()
        {
            string expectedTitle = "Conference Message Index";
            string actualTitle = driver.Title;

            Assert.AreEqual(expectedTitle, actualTitle);
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
