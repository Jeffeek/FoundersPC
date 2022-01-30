using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.SharedKernel.Interfaces;

namespace FoundersPC.Application.Services;

public class NullEmailService : IEmailService
{
    public Task<bool> SendToAsync(string email, string subject = "Unnamed", string content = "", bool html = false) => Task.FromResult(true);

    public Task SendToManyAsync(IEnumerable<string> emails, string subject = "Unnamed", string content = "", bool html = false) => Task.FromResult(true);

    public Task<bool> SendEntranceNotificationAsync(string email) => Task.FromResult(true);

    public Task<bool> SendRegistrationNotificationAsync(string email, string? subject = null) => Task.FromResult(true);

    public Task<bool> SendAPIAccessTokenAsync(string email, string token) => Task.FromResult(true);

    public Task<bool> SendNewPasswordAsync(string email, string password) => Task.FromResult(true);

    public Task<bool> SendAccessTokenBlockNotificationAsync(string email, string token, string? reason = null) => Task.FromResult(true);

    public Task<bool> SendBlockNotificationAsync(string email, string? reason = null) => Task.FromResult(true);

    public Task<bool> SendUnblockNotificationAsync(string email, string? reason = null) => Task.FromResult(true);

    public Task<bool> SendAccessTokenUnblockNotificationAsync(string email, string token, string? reason = null) => Task.FromResult(true);
}