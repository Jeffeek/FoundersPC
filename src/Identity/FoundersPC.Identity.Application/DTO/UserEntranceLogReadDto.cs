#region Using namespaces

using System;

#endregion

namespace FoundersPC.Identity.Application.DTO
{
    public class UserEntranceLogReadDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime Entrance { get; set; }
    }
}