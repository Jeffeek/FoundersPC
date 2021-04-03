#region Using namespaces

using System;
using System.Collections.Generic;

#endregion

namespace FoundersPC.Identity.Dto
{
    public class UserEntityReadDto
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public DateTime RegistrationDate { get; set; }

        public bool IsActive { get; set; }

        public bool IsBlocked { get; set; }

        public string Email { get; set; }

        public string HashedPassword { get; set; }

        public bool SendMessageOnEntrance { get; set; }

        public bool SendMessageOnApiRequest { get; set; }

        public RoleEntityReadDto Role { get; set; }

        public IEnumerable<ApiAccessUserTokenReadDto> Tokens { get; set; }
    }
}