#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.Identity.Application.DTO;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Services.User_Services
{
    public interface IUserSettingsService
    {
        Task<bool> ChangePasswordToAsync(int userId, string newPassword, string oldPassword);

        Task<bool> ChangePasswordToAsync(string userEmail, string newPassword, string oldPassword);

        Task<bool> GenerateAndChangePasswordToAsync(int userId);

        Task<bool> GenerateAndChangePasswordToAsync(string userEmail);

        Task<bool> ChangeLoginToAsync(string userEmail, string newLogin);

        Task<bool> ChangeLoginToAsync(int userId, string newLogin);

        Task<bool> ChangeNotificationsToAsync(string userEmail, UserNotificationsSettings settings);

        Task<bool> ChangeNotificationsToAsync(int userId, bool notificationOnEntrance, bool notificationOnApiRequest);
    }
}