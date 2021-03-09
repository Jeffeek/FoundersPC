#region Using namespaces

using System;

#endregion

namespace FoundersPC.Web.Models
{
    public sealed class ErrorViewModel
    {
        public string RequestId { get; set; }

        public string Error { get; set; }

        public bool ShowRequestId => !String.IsNullOrEmpty(RequestId);
    }
}