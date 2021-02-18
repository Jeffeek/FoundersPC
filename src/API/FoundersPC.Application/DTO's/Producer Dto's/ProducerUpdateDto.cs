﻿#region Using namespaces

using System;
using FoundersPC.Domain.Common.Interfaces.Hardware;

#endregion

namespace FoundersPC.Application
{
    public class ProducerUpdateDto : IProducer
    {
        public string ShortName { get; set; }

        public string FullName { get; set; }

        public string Country { get; set; }

        public string Website { get; set; }

        public DateTime? FoundationDate { get; set; }
    }
}