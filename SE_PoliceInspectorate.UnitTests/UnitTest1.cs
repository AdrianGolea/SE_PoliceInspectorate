using Microsoft.VisualStudio.TestTools.UnitTesting;
using SE_PoliceInspectorate.DataAccess.Model;

namespace SE_PoliceInspectorate.DataAccess.Tests
{
    [TestClass]
    public class ConferenceMessageTests
    {
        [TestMethod]
        public void SetContent_SetsContent()
        {
            // Arrange
            var message = new ConferenceMessage();
            var content = "Hello, everyone!";

            // Act
            message.Content = content;

            // Assert
            Assert.AreEqual(content, message.Content);
        }

        [TestMethod]
        public void SetFrom_SetsFrom()
        {
            // Arrange
            var message = new ConferenceMessage();
            var user = new User { Id = 1, UserName = "Alt_Nume" };

            // Act
            message.From = user;

            // Assert
            Assert.AreEqual(user, message.From);
        }

        [TestMethod]
        public void SetTo_SetsTo()
        {
            // Arrange
            var message = new ConferenceMessage();
       
            var user = new User { Id = 2, UserName = "JaneSmith" };

            // Act
            message.To = user;

            // Assert
            Assert.AreEqual(user, message.To);
        }
    }
}
