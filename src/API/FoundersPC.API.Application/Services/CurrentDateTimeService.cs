using System;
using FoundersPC.SharedKernel.Interfaces;

namespace FoundersPC.API.Application.Services
{
    public class CurrentDateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.UtcNow;
    }
}