#region Using namespaces

using System;
using System.Collections.Generic;
using FoundersPC.API.Domain.Entities.Identity.Logging;
using FoundersPC.API.Domain.Entities.Identity.Tokens;
using Microsoft.AspNetCore.Identity;

#endregion

namespace FoundersPC.API.Domain.Entities.Identity.Users
{
    public class ApplicationUser : IdentityUser<int>, IEquatable<ApplicationUser>
    {
        public string Login { get; set; } = default!;

        public DateTime RegistrationDate { get; set; } = default!;

        public int RoleId { get; set; } = default!;

        public bool IsActive { get; set; } = default!;

        public bool IsBlocked { get; set; } = default!;

        public bool SendMessageOnEntrance { get; set; } = default!;

        public bool SendMessageOnApiRequest { get; set; } = default!;

        public ApplicationRole ApplicationRole { get; set; } = default!;

        public ICollection<AccessToken> Tokens { get; set; } = default!;

        public ICollection<UserEntranceLog> Entrances { get; set; } = default!;

        public bool Equals(ApplicationUser other) => Email == other?.Email;
    }
}