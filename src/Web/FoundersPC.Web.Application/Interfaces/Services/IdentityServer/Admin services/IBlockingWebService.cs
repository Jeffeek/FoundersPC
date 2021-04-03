#region Using namespaces

using System.Threading.Tasks;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services
{
    public interface IBlockingWebService
    {
        Task<bool> BlockUserByIdAsync(int id, string adminToken);

        Task<bool> BlockUserByEmailAsync(string email, string adminToken);

        Task<bool> UnblockUserByIdAsync(int id, string adminToken);

        Task<bool> UnblockUserByEmailAsync(string email, string adminToken);

        Task<bool> MakeUserInactiveByIdAsync(int id, string adminToken);

        Task<bool> MakeUserInactiveByEmailAsync(string email, string adminToken);
    }
}