using Moq;
using global::SE_PoliceInspectorate.DataAccess.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SE_PoliceInspectorate.DataAccess.EF;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace SE_PoliceInspectorate.UnitTestsClassifiedFiles
{
    [TestClass]
        public class ClassifiedFilesRepositoryTests
        {
            [TestMethod]
            public void Search_WithValidSearchString_ReturnsMatchingClassifiedFiles()
            {
                // Arrange
                var dbContextOptions = new DbContextOptionsBuilder<PoliceInspectorateContext>()
                    .Options;

                var classifiedFiles = new List<ClassifiedFile>
            {
                new ClassifiedFile { InmateName = "John Doe", Felony = "Robbery", Title = "Case 1", Description = "Description 1", Sentence = "10 years" },
                new ClassifiedFile { InmateName = "Jane Smith", Felony = "Burglary", Title = "Case 2", Description = "Description 2", Sentence = "5 years" },
                new ClassifiedFile { InmateName = "Michael Johnson", Felony = "Fraud", Title = "Case 3", Description = "Description 3", Sentence = "3 years" }
            };

                var dbContextMock = new Mock<PoliceInspectorateContext>(dbContextOptions);
                dbContextMock.Setup(db => db.Set<ClassifiedFile>()).Returns(MockHelper.CreateDbSetMock(classifiedFiles).Object);

                var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
                httpContextAccessorMock.Setup(a => a.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(new Claim(ClaimTypes.NameIdentifier, "1")); // Modify the user ID based on your test scenario

                var repository = new ClassifiedFileRepository(dbContextMock.Object, httpContextAccessorMock.Object);
                var searchString = "Case 2";

                // Act
                var result = repository.Search(searchString);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.Count()); // Expecting a single matching file
                Assert.AreEqual("Case 2", result.First().Title);
            }
        }

        // Helper class to mock DbSet and IQueryable
        public static class MockHelper
        {
            public static Mock<DbSet<T>> CreateDbSetMock<T>(IEnumerable<T> elements) where T : class
            {
                var queryable = elements.AsQueryable();
                var dbSetMock = new Mock<DbSet<T>>();
                dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
                dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
                dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
                dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
                return dbSetMock;
            }
        }


    [TestClass]

    public class ClassifiedFilesRepositoryTests1
    {
        [TestMethod]
        public void GetCurrentUserId_ReturnsUserId()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<PoliceInspectorateContext>()
                .Options;

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(a => a.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier))
                .Returns(new Claim(ClaimTypes.NameIdentifier, "5")); // Modify the user ID based on your test scenario

            var repository = new ClassifiedFileRepository(null, httpContextAccessorMock.Object);

            // Act
            var result = repository.GetCurrentUserId();

            // Assert
            Assert.AreEqual(5, result); // Expecting the user ID to be 5
        }
    }

    [TestClass]
    public class ClassifiedFilesRepositoryTests2
    {
        [TestMethod]
        public void GetUsers_ReturnsAllUsers()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<PoliceInspectorateContext>()
                .Options;

            var users = new List<User>
            {
                new User { Id = 1, FirstName = "John" },
                new User { Id = 2, FirstName = "Jane" },
                new User { Id = 3, FirstName = "Michael" }
            };

            var dbContextMock = new Mock<PoliceInspectorateContext>(dbContextOptions);
            dbContextMock.Setup(db => db.Set<User>()).Returns(MockHelper.CreateDbSetMock(users).Object);

            var repository = new ClassifiedFileRepository(dbContextMock.Object, null);

            // Act
            var result = repository.GetUsers();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count()); // Expecting 3 users in the result
        }
    }

   
}


