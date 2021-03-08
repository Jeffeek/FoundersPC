#region Using namespaces

using System;

#endregion

namespace FoundersPC.AuthenticationShared
{
    public class ApiAccessUserTokenReadDto
    {
        public int Id { get; set; }

        public string HashedToken { get; set; }

        public DateTime StartEvaluationDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public bool IsBlocked { get; set; }
    }
}