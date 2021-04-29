#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.Identity.Application.Interfaces.Services.Mail_service;
using FoundersPC.Identity.Application.Interfaces.Services.Token_Services;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Domain.Entities.Users;
using FoundersPC.Identity.Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Identity.Services.Administration.Admin_Services
{
    // TODO: spread into another services
    // in thesis implementation
    /// <summary>
    ///     <inheritdoc cref="IAdminService"/>
    /// </summary>
    public class AdminService : IAdminService
    {
        private readonly IAccessUsersTokensService _accessUsersTokensService;
        private readonly IEmailService _emailService;
        private readonly ILogger<AdminService> _logger;
        private readonly IUnitOfWorkUsersIdentity _unitOfWork;

        public AdminService(IEmailService emailService,
                            IUnitOfWorkUsersIdentity unitOfWork,
                            IAccessUsersTokensService accessUsersTokensService,
                            ILogger<AdminService> logger)
        {
            _emailService = emailService;
            _unitOfWork = unitOfWork;
            _accessUsersTokensService = accessUsersTokensService;
            _logger = logger;
        }

        // todo: implement logger

        #region Make user inactive

        public async Task<bool> MakeUserInactiveAsync(int userId, bool sendNotification = true)
        {
            var user = await _unitOfWork.UsersRepository.GetByIdAsync(userId);

            return await MakeUserInactiveAsync(user, sendNotification);
        }

        public async Task<bool> MakeUserInactiveAsync(string email, bool sendNotification = true)
        {
            var user = await _unitOfWork.UsersRepository.GetUserByEmailAsync(email);

            return await MakeUserInactiveAsync(user, sendNotification);
        }

        private async Task<bool> MakeUserInactiveAsync(UserEntity user, bool sendNotification)
        {
            _logger.LogInformation($"{nameof(AdminService)} : {nameof(MakeUserInactiveAsync)} : trying to make user inactive");

            if (user is null)
            {
                _logger.LogWarning($"{nameof(AdminService)} : {nameof(MakeUserInactiveAsync)} : input user was null");

                return false;
            }

            if (!user.IsActive)
            {
                _logger.LogWarning($"{nameof(AdminService)} : {nameof(MakeUserInactiveAsync)} : input user was already inactive");

                return false;
            }

            if (user.Role.RoleTitle == ApplicationRoles.Administrator)
            {
                _logger.LogWarning($"{nameof(AdminService)} : {nameof(MakeUserInactiveAsync)} : input user was with administrator role");

                return false;
            }

            if (sendNotification)
            {
                var sendResult = await _emailService.SendBlockNotificationAsync(user.Email,
                                                                                "You've been blocked, you can't be unblocked.");

                if (!sendResult)
                    _logger.LogInformation($"{nameof(AdminService)} : {nameof(MakeUserInactiveAsync)} : unsuccessfully send message to {user.Email}");
            }

            user.IsActive = false;

            var updateResult = await _unitOfWork.UsersRepository.UpdateAsync(user);

            if (!updateResult)
            {
                _logger.LogInformation($"{nameof(AdminService)} : {nameof(MakeUserInactiveAsync)} : unsuccessfully changed user with email: {user.Email} to inactive");

                return false;
            }

            var saveChangesResult = await _unitOfWork.SaveChangesAsync() > 0;

            if (saveChangesResult)
                _logger.LogInformation($"{nameof(AdminService)} : {nameof(MakeUserInactiveAsync)} : successfully changed user with email: {user.Email} to inactive");

            return saveChangesResult;
        }

        #endregion

        // todo: implement logger

        #region Block / Unblock user

        #region Block

        public async Task<bool> BlockUserAsync(int userId, bool blockAllTokens = true, bool sendNotification = true)
        {
            var user = await _unitOfWork.UsersRepository.GetByIdAsync(userId);

            var changeStatusResult = await ChangeUserBlockStatusAsync(user, true, sendNotification);

            if (!changeStatusResult)
                return false;

            if (!blockAllTokens)
                return true;

            return await BlockAllUserTokensAsync(user.Id);
        }

        public async Task<bool> BlockUserAsync(string userEmail,
                                               bool blockAllTokens = true,
                                               bool sendNotification = true)
        {
            var user = await _unitOfWork.UsersRepository.GetUserByEmailAsync(userEmail);

            if (user is null)
                return false;

            return await BlockUserAsync(user.Id);
        }

        private async Task<bool> BlockAllUserTokensAsync(int userId)
        {
            var userTokens = await _unitOfWork.AccessTokensRepository.GetAllUserTokensAsync(userId);

            var blockingResults = new List<bool>();

            var futureDateTokens = userTokens.Where(token => !token.IsBlocked && token.ExpirationDate >= DateTime.Now);

            foreach (var token in futureDateTokens)
                blockingResults.Add(await BlockAccessTokenAsync(token.Id));

            return blockingResults.All(x => x);
        }

        #endregion

        #region Unblock

        public async Task<bool> UnBlockUserAsync(int userId, bool sendNotification = true)
        {
            var user = await _unitOfWork.UsersRepository.GetByIdAsync(userId);

            var changeStatusResult = await ChangeUserBlockStatusAsync(user, false, sendNotification);

            if (changeStatusResult)
                return await UnBlockAllUserTokensAsync(user.Id);

            return false;
        }

        public async Task<bool> UnBlockUserAsync(string userEmail, bool sendNotification = true)
        {
            var user = await _unitOfWork.UsersRepository.GetUserByEmailAsync(userEmail);

            var changeStatusResult = await ChangeUserBlockStatusAsync(user, false, sendNotification);

            if (changeStatusResult)
                return await UnBlockAllUserTokensAsync(user.Id);

            return false;
        }

        private async Task<bool> UnBlockAllUserTokensAsync(int userId)
        {
            var userTokens = await _unitOfWork.AccessTokensRepository.GetAllUserTokensAsync(userId);

            var unblocking = new List<bool>();

            var futureDateTokens = userTokens.Where(token => token.IsBlocked && token.ExpirationDate >= DateTime.Now);

            foreach (var token in futureDateTokens)
                unblocking.Add(await UnBlockAccessTokenAsync(token.Id));

            return unblocking.All(x => x);
        }

        #endregion

        /// <summary>
        ///     Blocks/Unblocks user
        /// </summary>
        /// <param name="user">User to change</param>
        /// <param name="block">true - blocking, false - unblocking</param>
        /// <param name="sendNotification">Send notification to user via email</param>
        /// <returns></returns>
        private async Task<bool> ChangeUserBlockStatusAsync(UserEntity user, bool block, bool sendNotification)
        {
            if (user is null)
                return false;

            if (!user.IsActive)
                return false;

            if (user.Role.RoleTitle == ApplicationRoles.Administrator)
                return false;

            user.IsBlocked = block;

            if (sendNotification)
            {
                if (block)
                    await _emailService.SendBlockNotificationAsync(user.Email,
                                                                   "You've been BLOCKED, you can be unblocked. Contact the administrator for reasons");
                else
                    await _emailService.SendUnBlockNotificationAsync(user.Email, "You've been UNBLOCKED.");
            }

            var updateResult = await _unitOfWork.UsersRepository.UpdateAsync(user);

            if (!updateResult)
                return false;

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        #endregion

        #region Block API tokenEntity

        public Task<bool> BlockAccessTokenAsync(int tokenId) =>
            _accessUsersTokensService.BlockAsync(tokenId);

        public Task<bool> BlockAccessTokenAsync(string token) =>
            _accessUsersTokensService.BlockAsync(token);

        /// <inheritdoc/>
        public Task<bool> UnBlockAccessTokenAsync(int tokenId) =>
            _accessUsersTokensService.UnBlockAsync(tokenId);

        /// <inheritdoc/>
        public Task<bool> UnBlockAccessTokenAsync(string token) =>
            _accessUsersTokensService.UnBlockAsync(token);

        #endregion
    }
}