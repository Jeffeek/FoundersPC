#region Using namespaces

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoundersPC.API.Domain.Entities;
using FoundersPC.IdentityEntities.Identity;

#endregion

namespace FoundersPC.API.Domain.Common.Base
{
    public abstract class HardwareEntityBase : IdentityItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ProducerId")]
        [Required]
        public int ProducerId { get; set; }

        [ForeignKey(nameof(ProducerId))]
        public ProducerEntity ProducerEntity { get; set; }

        [MaxLength(100)]
        [MinLength(0)]
        [DataType(DataType.Text)]
        [Column("Title")]
        [Required]
        public string Title { get; set; }
    }
}