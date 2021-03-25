#region Using namespaces

using System;

#endregion

namespace FoundersPC.Identity.Application.DTO
{
    public class AccessTokenLogReadDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime RequestDateTime { get; set; }
    }
}