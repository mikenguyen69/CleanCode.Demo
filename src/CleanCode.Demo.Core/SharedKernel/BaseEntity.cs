using System.Collections.Generic;

namespace CleanCode.Demo.Core.SharedKernel
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
    }
}
