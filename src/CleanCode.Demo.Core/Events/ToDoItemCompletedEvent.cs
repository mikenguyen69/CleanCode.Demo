using CleanCode.Demo.Core.Entities;
using CleanCode.Demo.Core.SharedKernel;

namespace CleanCode.Demo.Core.Events
{
    public class ToDoItemCompletedEvent : BaseDomainEvent
    {
        private ToDoItem completedItem;

        public ToDoItemCompletedEvent(ToDoItem item)
        {
            completedItem = item;
        }
    }
}
