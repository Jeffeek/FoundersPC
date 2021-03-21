#region Using namespaces

using System;

#endregion

namespace FoundersPC.WebIdentityShared
{
    public class ApplicationAccessToken
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string HashedToken { get; set; }

        public DateTime StartEvaluationDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public bool IsBlocked { get; set; }
    }
}