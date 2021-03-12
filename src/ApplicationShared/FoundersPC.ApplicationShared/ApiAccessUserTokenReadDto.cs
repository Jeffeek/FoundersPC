#region Using namespaces

using System;

#endregion

namespace FoundersPC.ApplicationShared
{
	public class ApiAccessUserTokenReadDto
	{
		public int Id { get; set; }

		public int UserId { get; set; }

		public string HashedToken { get; set; }

		public DateTime StartEvaluationDate { get; set; }

		public DateTime ExpirationDate { get; set; }

		public bool IsBlocked { get; set; }
	}
}