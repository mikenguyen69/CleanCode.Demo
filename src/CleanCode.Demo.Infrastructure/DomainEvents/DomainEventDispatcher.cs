using CleanCode.Demo.Core.Interfaces;
using CleanCode.Demo.Core.SharedKernel;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Demo.Infrastructure.DomainEvents
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private IContainer _container;

        public DomainEventDispatcher(IContainer container)
        {
            _container = container;
        }

        public void Dispatch(BaseDomainEvent domainEvent)
        {
            // Get handler type of the domain event
            var handlerType = typeof(IHandler<>).MakeGenericType(domainEvent.GetType());
            
            // Get Domain Event Handler of the domain event
            var domainEventHandlerType = typeof(DomainEventHandler<>).MakeGenericType(domainEvent.GetType());

            // Get all handlers of the handler type from IoC container
            var handlers = _container.GetAllInstances(handlerType);

            var domainEventHandlers = handlers
                .Cast<object>()
                .Select(handler => (DomainEventHandler)Activator.CreateInstance(domainEventHandlerType, handler));

            foreach (var handler in domainEventHandlers)
                handler.Handle(domainEvent);
        }

        private abstract class DomainEventHandler
        {
            public abstract void Handle(BaseDomainEvent domainEvent);
        }

        private class DomainEventHandler<T> : DomainEventHandler where T : BaseDomainEvent
        {
            private readonly IHandler<T> _handler;

            public DomainEventHandler(IHandler<T> handler)
            {
                _handler = handler;
            }

            public override void Handle(BaseDomainEvent domainEvent)
            {
                _handler.Handle((T)domainEvent);
            }
        }
    }
    

}
