#region Using namespaces

using System;

#endregion

namespace FoundersPC.Identity.Dto
{
    public class UserEntranceLogReadDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime Entrance { get; set; }
    }
}