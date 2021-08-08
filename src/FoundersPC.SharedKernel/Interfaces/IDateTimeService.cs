using System;

namespace FoundersPC.SharedKernel.Interfaces
{
    public interface IDateTimeService
    {
        DateTime Now { get; }
    }
}