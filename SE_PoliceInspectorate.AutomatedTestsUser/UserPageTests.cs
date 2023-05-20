using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SE_PoliceInspectorate.AutomatedTestsUser
{
    [TestClass]
    public class UserPageTests
    {
        private static IWebDriver _driver;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // Set up WebDriver initialization
            _driver = new ChromeDriver();
        }

        [TestMethod]
        public void TestCreateUser_ValidData_Success()
        {
            // Arrange
            _driver.Navigate().GoToUrl("https://localhost:7099/Users/Create");

            // Fill in user details
            _driver.FindElement(By.Id("UserName")).SendKeys("testuser");
            _driver.FindElement(By.Id("Email")).SendKeys("testuser@example.com");
            _driver.FindElement(By.Id("Password")).SendKeys("password");
            _driver.FindElement(By.Id("FirstName")).SendKeys("John");
            _driver.FindElement(By.Id("LastName")).SendKeys("Doe");

            // Submit the form
            _driver.FindElement(By.Id("submitButton")).Click();

            // Assert
            Assert.IsTrue(_driver.Url.Contains("Users/Index"), "User creation successful");
        }

        [TestMethod]
        public void TestCreateUser_InvalidData_Error()
        {
            // Arrange
            _driver.Navigate().GoToUrl("https://localhost:7099/Users/Create");

            // Fill in invalid user details
            _driver.FindElement(By.Id("UserName")).SendKeys("");
            _driver.FindElement(By.Id("Email")).SendKeys("testuser@example.com");
            _driver.FindElement(By.Id("Password")).SendKeys("password");
            _driver.FindElement(By.Id("FirstName")).SendKeys("John");
            _driver.FindElement(By.Id("LastName")).SendKeys("Doe");

            // Submit the form
            _driver.FindElement(By.Id("submitButton")).Click();

            // Assert
            Assert.IsTrue(_driver.PageSource.Contains("The UserName field is required."), "Validation error displayed");
        }

        [TestMethod]
        public void TestEditUser_ValidData_Success()
        {
            // Arrange
            _driver.Navigate().GoToUrl("https://localhost:7099/Users/Edit/6");

            // Modify user details
            _driver.FindElement(By.Id("UserName")).Clear();
            _driver.FindElement(By.Id("UserName")).SendKeys("modifieduser");
            _driver.FindElement(By.Id("FirstName")).Clear();
            _driver.FindElement(By.Id("FirstName")).SendKeys("Modified");
            _driver.FindElement(By.Id("LastName")).Clear();
            _driver.FindElement(By.Id("LastName")).SendKeys("User");

            // Submit the form
            _driver.FindElement(By.Id("submitButton")).Click();

            // Assert
            Assert.IsTrue(_driver.Url.Contains("Users/Index"), "User edit successful");
        }

        [TestMethod]
        public void TestEditUser_InvalidData_Error()
        {
            // Arrange
            _driver.Navigate().GoToUrl("https://localhost:7099/Users/Edit/6");

            // Modify user details with invalid data
            _driver.FindElement(By.Id("UserName")).Clear();
            _driver.FindElement(By.Id("UserName")).SendKeys("");
            _driver.FindElement(By.Id("FirstName")).Clear();
            _driver.FindElement(By.Id("FirstName")).SendKeys("Modified");
            _driver.FindElement(By.Id("LastName")).Clear();
            _driver.FindElement(By.Id("LastName")).SendKeys("User");

            // Submit the form
            _driver.FindElement(By.Id("submitButton")).Click();

            // Assert
            Assert.IsTrue(_driver.PageSource.Contains("The UserName field is required."), "Validation error displayed");
        }

        [TestMethod]
        public void TestDeleteUser_ConfirmDeletion_Success()
        {
            // Arrange
            _driver.Navigate().GoToUrl("https://localhost:7099/Users/Delete/6");

            // Confirm deletion
            _driver.FindElement(By.Id("deleteButton")).Click();

            // Assert
            Assert.IsTrue(_driver.Url.Contains("Users/Index"), "User deletion successful");
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            // Clean up WebDriver instance
            _driver.Quit();
        }
    }

}