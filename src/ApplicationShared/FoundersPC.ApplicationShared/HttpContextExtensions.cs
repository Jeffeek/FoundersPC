using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FoundersPC.ApplicationShared
{
    public static class HttpContextExtensions
    {
        public static string GetIpAddress(this HttpContext httpContext) => httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
    }
}
