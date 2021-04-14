﻿#region Using namespaces

using FoundersPC.API.Dto.Base.Interfaces;

#endregion

namespace FoundersPC.API.Dto
{
    public class SSDInsertDto : IProducerIdentifiable
    {
        public double Factor { get; set; }

        public string Interface { get; set; }

        public int Volume { get; set; }

        public string MicroScheme { get; set; }

        public int SequentialRead { get; set; }

        public int SequentialRecording { get; set; }

        public string Title { get; set; }

        public int ProducerId { get; set; }
    }
}