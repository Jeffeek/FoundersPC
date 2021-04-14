#region Using namespaces

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace FoundersPC.IdentityEntities.Identity
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