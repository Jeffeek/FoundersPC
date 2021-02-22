#region Using namespaces

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoundersPC.ApplicationShared.Identity;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Web.Data.Entities
{
    [Index(nameof(Id))]
    public class ApplicationUser : IdentityItem, IEquatable<ApplicationUser>
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

        //MinLength for MD5 hash
        [MinLength(24)]
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
        public ApplicationRole Role { get; set; }

        [DefaultValue(true)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public bool IsActive { get; set; }

        public bool Equals(ApplicationUser other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Email == other.Email;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return Equals((ApplicationUser)obj);
        }

        public override int GetHashCode() => Email != null ? Email.GetHashCode() : 0;
    }
}