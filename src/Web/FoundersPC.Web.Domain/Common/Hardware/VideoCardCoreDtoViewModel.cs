#region Using namespaces

using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.Web.Domain.Common.Hardware
{
    public class VideoCardCoreDtoViewModel
    {
        [Display(Prompt = nameof(TechProcess))]
        [Range(5, 48)]
        [Required]
        public int TechProcess { get; set; }

        [Display(Prompt = nameof(MaxResolution))]
        [RegularExpression("\\d{3,5}x\\d{3,5}")]
        [StringLength(20)]
        [Required]
        public string MaxResolution { get; set; }

        [Display(Prompt = nameof(MonitorsSupport))]
        [Range(0, 24)]
        [Required]
        public int MonitorsSupport { get; set; }

        [Display(Prompt = nameof(ConnectionInterface))]
        [StringLength(30)]
        [Required]
        public string ConnectionInterface { get; set; }

        [Display(Prompt = nameof(Codename))]
        [StringLength(30)]
        [Required]
        public string Codename { get; set; }

        [Display(Prompt = nameof(DirectX))]
        [StringLength(10)]
        [Required]
        public string DirectX { get; set; }

        public bool SLIOrCrossfire { get; set; }

        [Display(Prompt = nameof(ArchitectureTitle))]
        [StringLength(30)]
        [Required]
        public string ArchitectureTitle { get; set; }

        [Display(Prompt = nameof(Title))]
        [StringLength(100, MinimumLength = 1)]
        [Required]
        public string Title { get; set; }
    }
}