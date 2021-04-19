﻿#region Using namespaces

using System;
using System.ComponentModel.DataAnnotations;
using FoundersPC.API.Dto.Base.Interfaces;

#endregion

namespace FoundersPC.API.Dto
{
    public class CaseInsertDto : IProducerIdentifiable
    {
        public string Type { get; set; }

        public string WindowMaterial { get; set; }

        public string MaxMotherboardSize { get; set; }

        public string Material { get; set; }

        public bool TransparentWindow { get; set; }

        public string Color { get; set; }

        public int? Depth { get; set; }

        public int? Height { get; set; }

        public string Title { get; set; }

        public double? Weight { get; set; }

        public int? Width { get; set; }

        public int ProducerId { get; set; }
    }
}