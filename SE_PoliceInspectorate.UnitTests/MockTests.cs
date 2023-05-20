using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using SE_PoliceInspectorate.DataAccess.EF;
using SE_PoliceInspectorate.DataAccess.Model;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Identity;

namespace SE_PoliceInspectorate.DataAccess.Tests
{
    [TestClass]
    public class ConferenceMessageRepositoryMockTests
    {
        private PoliceInspectorateContext CreateDbContext()
        {
             var connectionString = "Police";

            var options = new DbContextOptionsBuilder<PoliceInspectorateContext>()
                .UseSqlServer(connectionString)
                .Options;

            return new PoliceInspectorateContext(options);
        }

        [TestMethod]
        public void GetCurrentUserId_Should_Return_Current_User_Id()
        {
            // Arrange
            var dbContext = new Mock<PoliceInspectorateContext>();
            dbContext.Setup(c => c.RoleClaims).Returns(Mock.Of<DbSet<IdentityRoleClaim<int>>>());

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var currentUserId = 1;
            var claim = new Claim(ClaimTypes.NameIdentifier, currentUserId.ToString());
            var claimsIdentity = new ClaimsIdentity(new List<Claim> { claim });
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            var repository = new ConferenceMessageRepository(dbContext.Object, httpContextAccessorMock.Object);

            // Act
            var userId = repository.GetCurrentUserId();

            // Assert
            Assert.AreEqual(currentUserId, userId);
        }

        [TestMethod]
        public void GetMessages_Should_Return_Messages()
        {
            // Arrange
            var dbContext = CreateDbContext();

            var currentUserId = 1;
            var receiverId = 2;
            var messages = new List<ConferenceMessage>
            {
                new ConferenceMessage { FromId = receiverId, ToId = currentUserId },
                new ConferenceMessage { FromId = currentUserId, ToId = receiverId },
                new ConferenceMessage { FromId = receiverId, ToId = currentUserId }
            };
            dbContext.ConferenceMessage.AddRange(messages);
            dbContext.SaveChanges();

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var repository = new ConferenceMessageRepository(dbContext, httpContextAccessorMock.Object);

            // Act
            var result = repository.GetMessages(receiverId);

            // Assert
            Assert.AreEqual(messages.Count, result.Count());
            CollectionAssert.AreEqual(messages, result.ToList());
        }

        [TestMethod]
        public void GetUsers_Should_Return_Users_Excluding_Current_User()
        {
            // Arrange
            var dbContext = CreateDbContext();

            var currentUserId = 1;
            var users = new List<User>
            {
                new User { Id = 2, FirstName = "User 2" },
                new User { Id = 3, FirstName = "User 3" },
                new User { Id = 4, FirstName = "User 4" }
            };
            dbContext.User.AddRange(users);
            dbContext.SaveChanges();

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var repository = new ConferenceMessageRepository(dbContext, httpContextAccessorMock.Object);

            // Act
            var result = repository.GetUsers();

            // Assert
            Assert.AreEqual(users.Count - 1, result.Count());
            CollectionAssert.AllItemsAreUnique((System.Collections.ICollection)result);
            CollectionAssert.DoesNotContain((System.Collections.ICollection)result, users.Single(u => u.Id == currentUserId));
        }
    }
}
