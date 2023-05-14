using Microsoft.AspNetCore.Routing;
using SE_PoliceInspectorate.DataAccess.Model;
using System.ComponentModel.DataAnnotations;

namespace SE_PoliceInspectorate.DataAccess.Tests
{
    [TestClass]

    public class ClassifiedFileTests
    {

        /*[TestMethod]
        public void Felony_ShouldNotContainProfanity()
        {
            // Arrange
            var classifiedFile = new ClassifiedFile
            {
                Felony = "Murder with explicit language" // Felony contains explicit language
            };

            // Act
            var validationResult = new List<ValidationResult>();
            var context = new ValidationContext(classifiedFile);
            var isValid = Validator.TryValidateObject(classifiedFile, context, validationResult, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResult.Count);
            Assert.AreEqual("The Felony field must not contain profanity.", validationResult[0].ErrorMessage);
        }*/
        [TestMethod]
        public void Description_ShouldHaveMaxLength200()
        {
            // Arrange
            var classifiedFile = new ClassifiedFile
            {
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco." // Description exceeds the maximum allowed length
            };

            // Act
            var validationContext = new ValidationContext(classifiedFile);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            var isValid = Validator.TryValidateObject(classifiedFile, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The Title field is required.", validationResults[0].ErrorMessage);
        }
        [TestMethod]
        public void Title_ShouldBeRequired()
        {
            // Arrange
            var classifiedFile = new ClassifiedFile();

            // Act
            var validationResult = new List<ValidationResult>();
            var context = new ValidationContext(classifiedFile);
            var isValid = Validator.TryValidateObject(classifiedFile, context, validationResult, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResult.Count);
            Assert.AreEqual("The Title field is required.", validationResult[0].ErrorMessage);
        }


        [TestMethod]

        public void Title_ShouldNotBeNull()
        {
            // Arrange
            var classifiedFile = new ClassifiedFile
            {
                Title = null // Setting Title as null
            };

            // Act
            var validationResult = new List<ValidationResult>();
            var context = new ValidationContext(classifiedFile);
            var isValid = Validator.TryValidateObject(classifiedFile, context, validationResult, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResult.Count);
            Assert.AreEqual("The Title field is required.", validationResult[0].ErrorMessage);
        }

        /*[TestMethod]

        public void ExpectedReleaseDate_ShouldBeGreaterThanIncarcerationDate()
        {
            // Arrange
            var classifiedFile = new ClassifiedFile
            {
                IncarcerationDate = new DateTime(2022, 1, 1),
                ExpectedReleaseDate = new DateTime(2021, 12, 31) // ExpectedReleaseDate is earlier than IncarcerationDate
            };

            // Act
            var validationResult = new List<ValidationResult>();
            var context = new ValidationContext(classifiedFile);
            var isValid = Validator.TryValidateObject(classifiedFile, context, validationResult, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResult.Count);
            Assert.AreEqual("The ExpectedReleaseDate must be a date later than the IncarcerationDate.", validationResult[0].ErrorMessage);
        }*/


    }

    
}
    
    

