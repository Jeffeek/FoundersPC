﻿#region Using namespaces

using FoundersPC.API.Application.Base.Interfaces;
using FoundersPC.API.Domain.Common.Interfaces.Hardware;

#endregion

namespace FoundersPC.API.Application
{
    public class RAMUpdateDto : IRAM, IProducerIdentiable
    {
        public int ProducerId { get; set; }

        public string MemoryType { get; set; }

        public int Frequency { get; set; }

        public string CASLatency { get; set; }

        public string Timings { get; set; }

        public double Voltage { get; set; }

        public bool XMP { get; set; }

        public bool ECC { get; set; }

        public int PCIndex { get; set; }

        public string Title { get; set; }
    }
}