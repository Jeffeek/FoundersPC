#region Using namespaces

using System.Threading.Tasks;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Services.User_Services
{
    public interface IUserStatusChangeService
    {
        #region Docs

        /// <summary>
        ///     Blocking the user account by id with all tokens he had
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="blockAllTokens"></param>
        /// <param name="sendNotification"></param>
        /// <returns></returns>

        #endregion

        Task<bool> BlockUserAsync(int userId, bool blockAllTokens = true, bool sendNotification = true);

        #region Docs

        /// <summary>
        ///     Blocking the user account by email with all tokens he had
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="blockAllTokens"></param>
        /// <param name="sendNotification"></param>
        /// <returns></returns>

        #endregion

        Task<bool> BlockUserAsync(string userEmail, bool blockAllTokens = true, bool sendNotification = true);

        #region Docs

        /// <summary>
        ///     Unblocking the user account by id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sendNotification"></param>
        /// <returns></returns>

        #endregion

        Task<bool> UnBlockUserAsync(int userId, bool sendNotification = true);

        #region Docs

        /// <summary>
        ///     Unblocking the user account by email
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="sendNotification"></param>
        /// <returns></returns>

        #endregion

        Task<bool> UnBlockUserAsync(string userEmail, bool sendNotification = true);

        #region Docs

        /// <summary>
        ///     Makes user inactive ('removes') from database
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sendNotification"></param>
        /// <returns></returns>

        #endregion

        Task<bool> MakeUserInactiveAsync(int userId, bool sendNotification = true);

        #region Docs

        /// <summary>
        ///     Makes user inactive ('removes') from database
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="sendNotification"></param>
        /// <returns></returns>

        #endregion

        Task<bool> MakeUserInactiveAsync(string userEmail, bool sendNotification = true);
    }
}