#region Using namespaces

using System;

#endregion

namespace FoundersPC.SharedKernel.Interfaces;

public interface IDateTimeService
{
    DateTime Now { get; }
}