using CleanCode.Demo.Core.SharedKernel;

namespace CleanCode.Demo.Core.Interfaces
{
    public interface IDomainEventDispatcher 
    {
        void Dispatch(BaseDomainEvent domainEvent);
    }
}
