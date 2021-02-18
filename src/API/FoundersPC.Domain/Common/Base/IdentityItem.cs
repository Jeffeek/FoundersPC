#region Using namespaces

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoundersPC.Domain.Common.Interfaces;

#endregion

namespace FoundersPC.Domain.Common.Base
{
    public abstract class IdentityItem : IIdentityItem
    {
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Key]
        public int Id { get; set; }
    }
}