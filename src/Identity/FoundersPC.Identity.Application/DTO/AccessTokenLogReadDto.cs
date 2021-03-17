#region Using namespaces

using System;

#endregion

namespace FoundersPC.Identity.Application.DTO
{
    public class AccessTokenLogReadDto
    {
        public int ApiAccessUsersTokenId { get; set; }

        public DateTime RequestDateTime { get; set; }
    }
}