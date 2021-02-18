using System;

namespace FoundersPC.Web.Models
{
	public sealed class ErrorViewModel
	{
		public string RequestId { get; set; }

		public bool ShowRequestId => !String.IsNullOrEmpty(RequestId);
	}
}