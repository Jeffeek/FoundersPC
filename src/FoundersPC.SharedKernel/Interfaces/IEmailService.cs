using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoundersPC.SharedKernel.Interfaces;

public interface IEmailService
{
    Task<bool> SendToAsync(string email, string subject = "Unnamed", string content = "", bool html = false);

    Task SendToManyAsync(IEnumerable<string> emails, string subject = "Unnamed", string content = "", bool html = false);

    Task<bool> SendEntranceNotificationAsync(string email);

    Task<bool> SendRegistrationNotificationAsync(string email, string? subject = null);

    Task<bool> SendAPIAccessTokenAsync(string email, string token);

    Task<bool> SendNewPasswordAsync(string email, string password);

    Task<bool> SendAccessTokenBlockNotificationAsync(string email, string token, string? reason = null);

    Task<bool> SendBlockNotificationAsync(string email, string? reason = null);

    Task<bool> SendUnblockNotificationAsync(string email, string? reason = null);

    Task<bool> SendAccessTokenUnblockNotificationAsync(string email, string token, string? reason = null);
}