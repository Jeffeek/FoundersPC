#region Using namespaces

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoundersPC.API.Domain.Common.Interfaces;
using FoundersPC.API.Domain.Entities.Hardware;
using FoundersPC.ApplicationShared.Identity;

#endregion

namespace FoundersPC.API.Domain.Common.Base
{
    public abstract class HardwareEntityBase : IdentityItem, IHardware
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ProducerId")]
        [Required]
        public int ProducerId { get; set; }

        [ForeignKey(nameof(ProducerId))]
        public Producer Producer { get; set; }

        [MaxLength(100)]
        [MinLength(0)]
        [DataType(DataType.Text)]
        [Column("Title")]
        [Required]
        public string Title { get; set; }
    }
}