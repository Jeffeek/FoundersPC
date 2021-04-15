using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoundersPC.Identity.Application.Interfaces.Services.User_Services
{
    public interface IUserStatusChangeService
    {
        /// <summary>
        ///     Blocking the user account by id with all tokens he had
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="blockAllTokens"></param>
        /// <param name="sendNotification"></param>
        /// <returns></returns>
        Task<bool> BlockUserAsync(int userId, bool blockAllTokens = true, bool sendNotification = true);

        /// <summary>
        ///     Blocking the user account by email with all tokens he had
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="blockAllTokens"></param>
        /// <param name="sendNotification"></param>
        /// <returns></returns>
        Task<bool> BlockUserAsync(string userEmail, bool blockAllTokens = true, bool sendNotification = true);

        /// <summary>
        ///     Unblocking the user account by id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sendNotification"></param>
        /// <returns></returns>
        Task<bool> UnBlockUserAsync(int userId, bool sendNotification = true);

        /// <summary>
        ///     Unblocking the user account by email
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="sendNotification"></param>
        /// <returns></returns>
        Task<bool> UnBlockUserAsync(string userEmail, bool sendNotification = true);

        /// <summary>
        ///     Makes user inactive ('removes') from database
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sendNotification"></param>
        /// <returns></returns>
        Task<bool> MakeUserInactiveAsync(int userId, bool sendNotification = true);

        /// <summary>
        ///     Makes user inactive ('removes') from database
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="sendNotification"></param>
        /// <returns></returns>
        Task<bool> MakeUserInactiveAsync(string userEmail, bool sendNotification = true);
    }
}
