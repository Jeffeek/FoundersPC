using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoundersPC.Domain.Common.Base;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Domain.Entities.Users
{
    [Index(nameof(Id))]
    public class User : IdentityItem
    {
        [MaxLength(20, ErrorMessage = "Max length for login is 20 symbols")]
        [MinLength(4, ErrorMessage = "Min length for login is 4 symbols")]
        [DataType(DataType.Text)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Login { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Wrong email address")]
        [Required]
        public string Email { get; set; }

        [MinLength(32)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Password)]
        [Required]
        public string PasswordHash { get; set; }

        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public bool IsEmailConfirmed { get; set; }

        [Required]
        public int RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; }

        [DefaultValue(true)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public bool IsActive { get; set; }
    }
}