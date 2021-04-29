#region Using namespaces

using System;

#endregion

namespace FoundersPC.Identity.Dto
{
    public class AccessTokenReadDto
    {
        public int Id { get; set; }

        public string HashedToken { get; set; }

        public int UserId { get; set; }

        public DateTime StartEvaluationDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public bool IsBlocked { get; set; }

        public bool IsActive =>
            !IsBlocked && ExpirationDate > DateTime.Now;
    }
}