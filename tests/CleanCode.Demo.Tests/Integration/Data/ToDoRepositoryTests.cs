using CleanCode.Demo.Core.Entities;
using CleanCode.Demo.Core.Interfaces;
using CleanCode.Demo.Infrastructure.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CleanCode.Demo.Tests.Integration.Data
{
    [TestClass]
    public class ToDoRepositoryTests
    {
        [TestMethod]
        public void Add_save_a_todo_via_context()
        {
            // Arrange
            var mockDispatcher = new Mock<IDomainEventDispatcher>();
            var mockSet = new Mock<DbSet<ToDoItem>>();
            var mockContext = new Mock<AppDbContext>(mockDispatcher.Object);

            mockContext.Setup(x => x.Set<ToDoItem>()).Returns(mockSet.Object);
            var repository = new EfRepository<ToDoItem>(mockContext.Object);

            // Act
            repository.Add(new ToDoItem());

            // Assert
            mockSet.Verify(x => x.Add(It.IsAny<ToDoItem>()), Times.Once());
            mockContext.Verify(x => x.SaveChanges(), Times.Once());
        }

        //[TestMethod]
        //public void Update_change_a_todo_via_context() 
        //{
        //    // Arrange 
        //    var mockDispatcher = new Mock<IDomainEventDispatcher>();
        //    var mockSet = new Mock<DbSet<ToDoItem>>();
        //    var existingToDo = new ToDoItem { Id = 1, Title = "Test 1" };
        //    var updatedToDo = new ToDoItem { Id = 1, Title = "Updated Test 1" };
        //    mockSet.SetReturnsDefault(new[] { existingToDo });

        //    var mockContext = new Mock<AppDbContext>(mockDispatcher.Object);

        //    mockContext.Setup(x => x.Set<ToDoItem>()).Returns(mockSet.Object);

        //    var repository = new EfRepository<ToDoItem>(mockContext.Object);

        //    // Act 
        //    repository.Update(updatedToDo);

        //    // Assert
        //    mockContext.Verify(x => x.Entry(existingToDo), Times.Once);
        //}

        [TestMethod]
        public void List_return_three_todoitems()
        {
            // Arrange
            var mockDispatcher = new Mock<IDomainEventDispatcher>();
            var existingToDo = new ToDoItem { Id = 1, Title = "Test 1" };

            var data = new List<ToDoItem>()
            {
                new ToDoItem {Id = 1, Title = "AAA"},
                new ToDoItem {Id = 2, Title = "CCC"},
                new ToDoItem {Id = 3, Title = "BBB"}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ToDoItem>>();
            mockSet.As<IQueryable<ToDoItem>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ToDoItem>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ToDoItem>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ToDoItem>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<AppDbContext>(mockDispatcher.Object);

            mockContext.Setup(x => x.Set<ToDoItem>()).Returns(mockSet.Object);

            var repository = new EfRepository<ToDoItem>(mockContext.Object);

            // Act
            var toDoItems = repository.List();

            // Assert
            Assert.AreEqual(3, toDoItems.Count);
            Assert.AreEqual("AAA", toDoItems[0].Title);
            Assert.AreEqual("CCC", toDoItems[1].Title);
            Assert.AreEqual("BBB", toDoItems[2].Title);
        }
    }
}
