using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using SE_PoliceInspectorate.Controllers;
using SE_PoliceInspectorate.DataAccess.Abstractions;
using SE_PoliceInspectorate.DataAccess.Model;
using Microsoft.AspNetCore.Mvc;

namespace SE_PoliceInspectorate.AutomatedTestsClassifiedFiles
{
    [TestClass]
    public class ClassifiedFilesControllerTests
    {
        private IWebDriver _driver;
        private ClassifiedFilesController _controller;
        private IClassifiedFilesRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            // Set up the Selenium ChromeDriver
            _driver = new ChromeDriver();

            // Set up the controller and repository
            _repository = new MockClassifiedFilesRepository();
            _controller = new ClassifiedFilesController(_repository);
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Close the Selenium ChromeDriver
            _driver.Quit();
            _driver.Dispose();
        }

        [TestMethod]
        public async Task Index_WithNoSearchString_ReturnsViewWithFiles()
        {
            // Arrange
            var expectedFiles = new List<ClassifiedFile> { new ClassifiedFile { Id = 1, Title = "File 1" }, new ClassifiedFile { Id = 2, Title = "File 2" } };
            ((MockClassifiedFilesRepository)_repository).SetFiles(expectedFiles);

            // Act
            var result = await _controller.Index(null) as ViewResult;
            var model = result.Model as List<ClassifiedFile>;

            // Assert
            Assert.AreEqual(expectedFiles.Count, model.Count);
            Assert.IsTrue(expectedFiles.All(f => model.Any(m => m.Id == f.Id && m.Title == f.Title)));
        }

        [TestMethod]
        public async Task Details_WithValidId_ReturnsViewWithFile()
        {
            // Arrange
            var fileId = 1;
            var expectedFile = new ClassifiedFile { Id = fileId, Title = "File 1" };
            ((MockClassifiedFilesRepository)_repository).SetFile(expectedFile);

            // Act
            var result = await _controller.Details(fileId) as ViewResult;
            var model = result.Model as ClassifiedFile;

            // Assert
            Assert.IsNotNull(model);
            Assert.AreEqual(expectedFile.Id, model.Id);
            Assert.AreEqual(expectedFile.Title, model.Title);
        }

        [TestMethod]
        public async Task Create_WithValidFile_CreatesFileAndRedirectsToIndex()
        {
            // Arrange
            var file = new ClassifiedFile { Title = "New File" };
            var expectedFilesCount = ((MockClassifiedFilesRepository)_repository).GetAll().Count() + 1;

            // Act
            var result = await _controller.Create(file) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(nameof(ClassifiedFilesController.Index), result.ActionName);

            var files = ((MockClassifiedFilesRepository)_repository).GetAll();
            Assert.AreEqual(expectedFilesCount, files.Count());
            Assert.IsTrue(files.Any(f => f.Title == file.Title));
        }

        [TestMethod]
        public async Task Edit_WithValidIdAndFile_UpdatesFileAndRedirectsToIndex()
        {
            // Arrange
            var fileId = 1;
            var file = new ClassifiedFile { Id = fileId, Title = "Updated File" };
            var expectedFile = ((MockClassifiedFilesRepository)_repository).GetById(fileId);
            var expectedFilesCount = ((MockClassifiedFilesRepository)_repository).GetAll().Count();

            // Act
            var result = await _controller.Edit(fileId, file) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(nameof(ClassifiedFilesController.Index), result.ActionName);

            var updatedFile = ((MockClassifiedFilesRepository)_repository).GetById(fileId);
            Assert.IsNotNull(updatedFile);
            Assert.AreEqual(file.Title, updatedFile.Title);
            Assert.AreEqual(expectedFile.CreatedById, updatedFile.CreatedById);
            Assert.AreNotEqual(expectedFile.UpdatedAt, updatedFile.UpdatedAt);
        }

        [TestMethod]
        public async Task Delete_WithValidId_DeletesFileAndRedirectsToIndex()
        {
            // Arrange
            var fileId = 1;
            var expectedFilesCount = ((MockClassifiedFilesRepository)_repository).GetAll().Count() - 1;

            // Act
            var result = await _controller.DeleteConfirmed(fileId) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(nameof(ClassifiedFilesController.Index), result.ActionName);

            var files = ((MockClassifiedFilesRepository)_repository).GetAll();
            Assert.AreEqual(expectedFilesCount, files.Count());
            Assert.IsFalse(files.Any(f => f.Id == fileId));
        }

        // Helper class for mocking IClassifiedFilesRepository
        // Helper class for mocking IClassifiedFilesRepository
        // Helper class for mocking IClassifiedFilesRepository


        private class MockClassifiedFilesRepository : IClassifiedFilesRepository
        {
            private List<ClassifiedFile> _files;

            public MockClassifiedFilesRepository()
            {
                _files = new List<ClassifiedFile>();
            }

            public void SetFiles(List<ClassifiedFile> files)
            {
                _files = files;
            }

            public void SetFile(ClassifiedFile file)
            {
                _files.Add(file);
            }

            public IQueryable<ClassifiedFile> GetAll()
            {
                return _files.AsQueryable();
            }

            public ClassifiedFile GetById(int id)
            {
                return _files.FirstOrDefault(f => f.Id == id);
            }

            public Task<ClassifiedFile> GetByIdAsync(int id)
            {
                return Task.FromResult(_files.FirstOrDefault(f => f.Id == id));
            }

            public ClassifiedFile Add(ClassifiedFile entity)
            {
                // No implementation needed for mock repository
                return entity;
            }


            public ClassifiedFile Update(ClassifiedFile entity)
            {
                // No implementation needed for mock repository
                return entity;
            }




            public Task<bool> Delete(int id)
            {
                // No implementation needed for mock repository
                return Task.FromResult(true);
            }




            public Task SaveAsync()
            {
                return Task.CompletedTask;
            }

            public IQueryable<User> GetUsers()
            {
                // Return empty users for the purpose of testing
                return Enumerable.Empty<User>().AsQueryable();
            }

            public IQueryable<ClassifiedFile> Search(string? searchString)
            {
                throw new NotImplementedException();
            }

            public int GetCurrentUserId()
            {
                throw new NotImplementedException();
            }

            // Implement the remaining interface methods if required for your tests
        }


    }
}