using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoundersPC.Web.Application.Interfaces.Services.HardwareApi
{
    public interface IHardwareApiService
    {
        Task<string> GetStringAsync(string entityType, string token);
    }
}
