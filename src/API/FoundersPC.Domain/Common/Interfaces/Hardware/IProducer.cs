using System;

namespace FoundersPC.Domain.Common.Interfaces.Hardware
{
    public interface IProducer
    {
        string ShortName { get; set; }

        string FullName { get; set; }

        string Country { get; set; }

        string Website { get; set; }

        DateTime? FoundationDate { get; set; }
    }
}