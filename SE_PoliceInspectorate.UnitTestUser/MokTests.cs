using Moq;
using SE_PoliceInspectorate.DataAccess.EF;
using SE_PoliceInspectorate.DataAccess.Model;

[TestClass]
public class MokTests
{
    private Mock<PoliceInspectorateContext> dbContextMock;
    private UserRepository userRepository;

    [TestInitialize]
    public void Setup()
    {
        dbContextMock = new Mock<PoliceInspectorateContext>();
        userRepository = new UserRepository(dbContextMock.Object);
    }

    [TestMethod]
    public void GetAll_Returns_Users_With_PoliceStation()
    {
        // Arrange
        var users = new[]
        {
            new User { Id = 1, UserName = "user1", PoliceStationId = 1 },
            new User { Id = 2, UserName = "user2", PoliceStationId = 2 }
        }.AsQueryable();

        var policeStations = new[]
        {
            new PoliceStation { Id = 1, Title = "Station 1" },
            new PoliceStation { Id = 2, Title = "Station 2" }
        }.AsQueryable();

        dbContextMock.Setup(m => m.Set<User>()).Returns((Microsoft.EntityFrameworkCore.DbSet<User>)users);
        dbContextMock.Setup(m => m.Set<PoliceStation>()).Returns((Microsoft.EntityFrameworkCore.DbSet<PoliceStation>)policeStations);

        // Act
        var result = userRepository.GetAll();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(users.Count(), result.Count());
        Assert.IsTrue(result.All(user => user.PoliceStation != null));
    }

    [TestMethod]
    public void GetStations_Returns_PoliceStations()
    {
        // Arrange
        var policeStations = new[]
        {
            new PoliceStation { Id = 1, Title = "Station 1" },
            new PoliceStation { Id = 2, Title = "Station 2" }
        }.AsQueryable();

        dbContextMock.Setup(m => m.Set<PoliceStation>()).Returns((Microsoft.EntityFrameworkCore.DbSet<PoliceStation>)policeStations);

        // Act
        var result = userRepository.GetStations();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(policeStations.Count(), result.Count());
    }

    [TestMethod]
    public void Update_Returns_Updated_User()
    {
        // Arrange
        var existingUser = new User { Id = 1, UserName = "user1", PoliceStationId = 1 };
        var updatedUser = new User { Id = 1, UserName = "user1-updated", PoliceStationId = 2 };

        dbContextMock.Setup(m => m.Set<User>()).Returns((Microsoft.EntityFrameworkCore.DbSet<User>)new[] { existingUser }.AsQueryable());

        // Act
        var result = userRepository.Update(updatedUser);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(existingUser.Id, result.Id);
        Assert.AreEqual(updatedUser.UserName, result.UserName);
        Assert.AreEqual(updatedUser.PoliceStationId, result.PoliceStationId);

        dbContextMock.Verify(m => m.SaveChanges(), Times.Once());
    }
}
