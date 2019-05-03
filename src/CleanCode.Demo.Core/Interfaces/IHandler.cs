using CleanCode.Demo.Core.SharedKernel;

namespace CleanCode.Demo.Core.Interfaces
{
    public interface IHandler<T> where T : BaseDomainEvent
    {
        void Handle(T domainEvent);
    }
}
