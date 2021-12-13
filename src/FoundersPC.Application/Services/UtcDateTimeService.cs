#region Using namespaces

using System;
using FoundersPC.SharedKernel.Interfaces;

#endregion

namespace FoundersPC.Application.Services;

public class UtcDateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.UtcNow;
}