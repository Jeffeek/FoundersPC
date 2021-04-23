#region Using namespaces

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.Web.Domain.Common.Hardware
{
    public class CaseDtoViewModel
    {
        [Display(Prompt = nameof(Type))]
        [StringLength(40, MinimumLength = 3)]
        [Required]
        public string Type { get; set; }

        [Display(Prompt = nameof(WindowMaterial))]
        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string WindowMaterial { get; set; }

        [Display(Prompt = nameof(MaxMotherboardSize))]
        [StringLength(20, MinimumLength = 3)]
        [Required]
        public string MaxMotherboardSize { get; set; }

        [Display(Prompt = nameof(Material))]
        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Material { get; set; }

        public bool TransparentWindow { get; set; }

        [Display(Prompt = nameof(Color))]
        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Color { get; set; }

        [Display(Prompt = nameof(Depth))]
        [Range(1, Int32.MaxValue)]
        public int Depth { get; set; }

        public bool IsDepthEmpty { get; set; }

        [Display(Prompt = nameof(Height))]
        [Range(1, Int32.MaxValue)]
        public int Height { get; set; }

        public bool IsHeightEmpty { get; set; }

        [Display(Prompt = nameof(Title))]
        [StringLength(100, MinimumLength = 1)]
        [Required]
        public string Title { get; set; }

        [Display(Prompt = nameof(Weight))]
        [Range(1, Double.MaxValue)]
        public double Weight { get; set; }

        public bool IsWeightEmpty { get; set; }

        [Display(Prompt = nameof(Width))]
        [Range(1, Int32.MaxValue)]
        public int Width { get; set; }

        public bool IsWidthEmpty { get; set; }

        [Display(Prompt = nameof(ProducerId))]
        [Range(1, Int64.MaxValue)]
        [Required]
        public int ProducerId { get; set; }
    }
}