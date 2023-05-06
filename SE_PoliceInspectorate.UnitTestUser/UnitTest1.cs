

using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SE_PoliceInspectorate.DataAccess.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SE_PoliceInspectorate.UnitTests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void User_ShouldHaveFirstNameProperty()
        {
            // Arrange
            var user = new User();

            // Act
            user.FirstName = "John";

            // Assert
            Assert.AreEqual("John", user.FirstName);
        }

        [TestMethod]
        public void User_ShouldHaveLastNameProperty()
        {
            // Arrange
            var user = new User();

            // Act
            user.LastName = "Doe";

            // Assert
            Assert.AreEqual("Doe", user.LastName);
        }

        [TestMethod]
        public void User_ShouldHavePoliceStationNavigationProperty()
        {
            // Arrange
            var user = new User();
            var policeStation = new PoliceStation();

            // Act
            user.PoliceStation = policeStation;

            // Assert
            Assert.AreEqual(policeStation, user.PoliceStation);
        }
    }

}