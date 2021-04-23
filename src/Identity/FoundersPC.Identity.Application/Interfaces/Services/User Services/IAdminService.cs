#region Using namespaces

using System.Threading.Tasks;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Services.User_Services
{
    public interface IAdminService : IUserStatusChangeService
    {
        /// <summary>
        ///     Blocking API tokenEntity by id
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        Task<bool> BlockAccessTokenAsync(int tokenId);

        /// <summary>
        ///     Blocking API tokenEntity by tokenEntity itself
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> BlockAccessTokenAsync(string token);
    }
}