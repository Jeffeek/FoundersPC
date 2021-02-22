﻿using System.Threading.Tasks;
using FoundersPC.Identity.Application.DTO;

namespace FoundersPC.Identity.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserEntityReadDto> TryToFindUser(string emailOrLogin, string rawPassword);

        Task<bool> TryToRegisterUser(string email, string rawPassword);
    }
}
