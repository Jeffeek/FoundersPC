#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Services.Mail_service
{
    public interface IMailService
    {
        Task<bool> SendToAsync(string email, string subject, string content, bool html);

        Task<bool> SendToManyAsync(IEnumerable<string> emails, string subject, string content, bool html);
    }
}