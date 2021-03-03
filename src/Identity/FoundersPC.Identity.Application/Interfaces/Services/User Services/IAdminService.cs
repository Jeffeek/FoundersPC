using System.Threading.Tasks;

namespace FoundersPC.Identity.Application.Interfaces.Services.User_Services
{
    public interface IAdminService
    {
        Task<bool> RegisterNewManagerAsync(string email, string password);

        /// <summary>
        /// Register new manager by email and generates a password(sends via email too)
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<bool> RegisterNewManagerAsync(string email);

        /// <summary>
        /// Blocking the user account with all tokens he had
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="blockAllTokens"></param>
        /// <param name="sendNotification"></param>
        /// <returns></returns>
        Task<bool> BlockUserAsync(int userId, bool blockAllTokens = true, bool sendNotification = true);

        /// <summary>
        /// Makes user inactive ('removes') from database
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sendNotification"></param>
        /// <returns></returns>
        Task<bool> MakeUserInactiveAsync(int userId, bool sendNotification = true);

        /// <summary>
        /// Blocking API token by id
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        Task<bool> BlockAPITokenAsync(int tokenId);

        /// <summary>
        /// Blocking API token by token itself
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> BlockAPITokenAsync(string token);
    }
}
