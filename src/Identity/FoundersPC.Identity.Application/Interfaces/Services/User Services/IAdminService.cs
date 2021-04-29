#region Using namespaces

using System.Threading.Tasks;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Services.User_Services
{
    public interface IAdminService : IUserStatusChangeService
    {
        /// <summary>
        ///     Blocking API token by id
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        Task<bool> BlockAccessTokenAsync(int tokenId);

        /// <summary>
        ///     Blocking API token by tokenEntity itself
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> BlockAccessTokenAsync(string token);

        /// <summary>
        ///     UnBlocking API token by id
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        Task<bool> UnBlockAccessTokenAsync(int tokenId);

        /// <summary>
        ///     UnBlocking API token by tokenEntity itself
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> UnBlockAccessTokenAsync(string token);
    }
}