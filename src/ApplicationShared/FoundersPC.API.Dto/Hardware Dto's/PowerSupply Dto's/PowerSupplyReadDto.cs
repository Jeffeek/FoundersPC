﻿#region Using namespaces

using FoundersPC.API.Dto.Base.Interfaces;

#endregion

namespace FoundersPC.API.Dto
{
    public class PowerSupplyReadDto : IProducerableDto
    {
        public int Id { get; set; }

        public int Power { get; set; }

        public int? Efficiency { get; set; }

        public string MotherboardPowering { get; set; }

        public bool IsModular { get; set; }

        public bool? CPU4PIN { get; set; }

        public bool? CPU8PIN { get; set; }

        public int FanDiameter { get; set; }

        public bool Certificate80PLUS { get; set; }

        public bool PFC { get; set; }

        public string Title { get; set; }

        public int ProducerId { get; set; }

        public ProducerReadDto Producer { get; set; }
    }
}