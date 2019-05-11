using AutoMapper;
using CleanCode.Demo.Core.Entities;
using CleanCode.Demo.Core.Interfaces;
using CleanCode.Demo.WebAPI.Controllers;
using CleanCode.Demo.WebAPI.DTO;
using CleanCode.Demo.WebAPI.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http;
using System.Web.Http.Results;

namespace CleanCode.Demo.Tests.Integration.Api
{
    [TestClass]
    public class ToDoItemsControllerTests
    {
        private readonly IMapper _mapper = AutoMapperConfig.GetMapper();

        [TestMethod]
        public void GetReturnsNotFound()
        {
            // Arrange 
            var mockRepository = new Mock<IRepository<ToDoItem>>();
            var controller = new ToDoItemsController(mockRepository.Object, _mapper);

            // Act
            IHttpActionResult actionResult = controller.Get(10);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetReturnsProductWithTheSameID()
        {
            // Arrange 
            var mockRepository = new Mock<IRepository<ToDoItem>>();
            mockRepository.SetReturnsDefault(new ToDoItem() { Id = 10 });

            var controller = new ToDoItemsController(mockRepository.Object, _mapper);

            // Act
            IHttpActionResult actionResult = controller.Get(10);
            var contentResult = actionResult as OkNegotiatedContentResult<ToDoItemDTO>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(10, contentResult.Content.Id);
        }

        [TestMethod]
        public void DeleteReturnsOkNegotiatedContentResultDTO()
        {
            // Arrange 
            var mockRepository = new Mock<IRepository<ToDoItem>>();
            mockRepository.SetReturnsDefault(new ToDoItem() { Id = 10 });

            var controller = new ToDoItemsController(mockRepository.Object, _mapper);

            // Act
            IHttpActionResult actionResult = controller.Delete(10);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<ToDoItemDTO>));
        }

        [TestMethod]
        public void PostMethodReturnsIteSavedItem()
        {
            // Arrange 
            var mockRepository = new Mock<IRepository<ToDoItem>>();
            var controller = new ToDoItemsController(mockRepository.Object, _mapper);

            // Act 
            IHttpActionResult actionResult = controller.Post(new ToDoItemDTO { Id = 10, Title = "Test Action"});
            var postedResult = actionResult as OkNegotiatedContentResult<ToDoItemDTO>;

            // Assert
            Assert.IsNotNull(postedResult);
            Assert.IsNotNull(postedResult.Content);
            Assert.AreEqual(10, postedResult.Content.Id);
            Assert.AreEqual("Test Action", postedResult.Content.Title);
        }

        [TestMethod]
        public void PutMethodReturnsModifiedItem()
        {
            // Arrange 
            var mockRepository = new Mock<IRepository<ToDoItem>>();
            mockRepository.SetReturnsDefault(new ToDoItem { Id = 10, Title = "Item", Description = "Description" });
            var controller = new ToDoItemsController(mockRepository.Object, _mapper);

            var modifiedItem = new ToDoItemDTO { Title = "Updated Item", Description = "Updated Description"};
            // Act 
            IHttpActionResult actionResult = controller.Put(10, modifiedItem);
            var updatedResult = actionResult as OkNegotiatedContentResult<ToDoItemDTO>;

            // Assert
            Assert.IsNotNull(updatedResult);
            Assert.IsNotNull(updatedResult.Content);
            Assert.AreEqual(modifiedItem.Description, updatedResult.Content.Description);
            Assert.AreEqual(modifiedItem.Title, updatedResult.Content.Title);
        }
    }
}
