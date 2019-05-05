using CleanCode.Demo.Core.Interfaces;
using CleanCode.Demo.Infrastructure.Data;

namespace CleanCode.Demo.WebAPI.Models
{
    public class ToDoItemContext : AppDbContext
    {
        public ToDoItemContext(IDomainEventDispatcher domainEventDispatcher) : base(domainEventDispatcher){ }
    }
}
