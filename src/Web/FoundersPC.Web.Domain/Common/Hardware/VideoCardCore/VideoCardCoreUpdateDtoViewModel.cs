#region Using namespaces

using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.Web.Domain.Common.Hardware.VideoCardCore
{
    public class VideoCardCoreUpdateDtoViewModel
    {
        [Range(5, 48)]
        [Required]
        public int TechProcess { get; set; }

        [RegularExpression("\\d{3,5}x\\d{3,5}")]
        [StringLength(20)]
        [Required]
        public string MaxResolution { get; set; }

        [Range(0, 24)]
        [Required]
        public int MonitorsSupport { get; set; }

        [StringLength(30)]
        [Required]
        public string ConnectionInterface { get; set; }

        [StringLength(30)]
        [Required]
        public string Codename { get; set; }

        [StringLength(10)]
        [Required]
        public string DirectX { get; set; }

        public bool SLIOrCrossfire { get; set; }

        [StringLength(30)]
        [Required]
        public string ArchitectureTitle { get; set; }

        [StringLength(100, MinimumLength = 1)]
        [Required]
        public string Title { get; set; }
    }
}