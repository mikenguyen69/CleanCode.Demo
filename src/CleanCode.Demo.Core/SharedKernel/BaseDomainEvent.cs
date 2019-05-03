using System;

namespace CleanCode.Demo.Core.SharedKernel
{
    public class BaseDomainEvent
    {
        public DateTime DateOccurred { get; protected set; } = DateTime.Now;
    }
}
