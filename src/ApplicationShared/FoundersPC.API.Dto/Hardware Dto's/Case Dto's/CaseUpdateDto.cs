#region Using namespaces

using System;
using System.ComponentModel.DataAnnotations;
using FoundersPC.API.Dto.Base.Interfaces;

#endregion

namespace FoundersPC.API.Dto
{
    public class CaseUpdateDto : IProducerIdentifiable
    {
        [StringLength(40, MinimumLength = 3)]
        [Required]
        public string Type { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string WindowMaterial { get; set; }

        [StringLength(20, MinimumLength = 3)]
        [Required]
        public string MaxMotherboardSize { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Material { get; set; }

        public bool TransparentWindow { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Color { get; set; }

        [Range(1, Int32.MaxValue)]
        public int? Depth { get; set; }

        [Range(1, Int32.MaxValue)]
        public int? Height { get; set; }

        [StringLength(100, MinimumLength = 1)]
        [Required]
        public string Title { get; set; }

        [Range(0, Double.MaxValue)]
        public double? Weight { get; set; }

        [Range(0, Int32.MaxValue)]
        public int? Width { get; set; }

        [Range(1, Int32.MaxValue)]
        [Required]
        public int ProducerId { get; set; }
    }
}