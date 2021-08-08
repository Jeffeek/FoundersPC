#region Using namespaces

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FoundersPC.API.Domain.Common;
using Microsoft.AspNetCore.Identity;

#endregion

namespace FoundersPC.API.Domain.Entities.Identity.Users
{
    public class ApplicationRole : IdentityRole<int>, IEquatable<ApplicationRole>
    {
        public ICollection<ApplicationUser> Users { get; set; } = default!;

        public bool Equals(ApplicationRole other) => Name == other?.Name;
    }
}