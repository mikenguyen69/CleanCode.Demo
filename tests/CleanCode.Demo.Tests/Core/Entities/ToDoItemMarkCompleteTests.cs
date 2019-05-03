using CleanCode.Demo.Core.Entities;
using CleanCode.Demo.Core.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CleanCode.Demo.Tests.Core.Entities
{
    [TestClass]
    public class ToDoItemMarkCompleteTests
    {
        [TestMethod]
        public void ShouldSetIsDoneToTrue()
        {
            var item = new ToDoItem();

            item.MarkComplete();

            Assert.IsTrue(item.IsDone);
        } 

        [TestMethod]
        public void ShouldRaiseToDoItemCompletedEvent()
        {
            var item = new ToDoItem();

            item.MarkComplete();

            Assert.AreEqual(1, item.Events.Count);
            Assert.IsInstanceOfType(item.Events.FirstOrDefault(), typeof(ToDoItemCompletedEvent));
        }
    }
}
