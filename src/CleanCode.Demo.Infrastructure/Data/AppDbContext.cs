using CleanCode.Demo.Core.Entities;
using CleanCode.Demo.Core.Interfaces;
using CleanCode.Demo.Core.SharedKernel;
using System.Data.Entity;
using System.Linq;

namespace CleanCode.Demo.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        private IDomainEventDispatcher _dispatcher;
        
        public AppDbContext(IDomainEventDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }
        
        // using virtual to allow the mocked implementation
        public virtual DbSet<ToDoItem> ToDoItems { get; set; }

        public override int SaveChanges()
        {
            int result = base.SaveChanges();

            // handle the domain events
            var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
                .Select(x => x.Entity)
                .Where(x => x.Events.Any())
                .ToArray();

            foreach(var entity in entitiesWithEvents)
            {
                var events = entity.Events.ToArray();
                entity.Events.Clear();

                foreach(var domainEvent in events)
                {
                    _dispatcher.Dispatch(domainEvent);
                }
            }

            return result;
        }

    }
}
