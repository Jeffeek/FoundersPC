#region Using namespaces

using System;
using System.Collections.Generic;
using Bogus;
using FoundersPC.Application.Services;
using FoundersPC.Application.Settings;
using FoundersPC.Domain.Entities.Identity.Tokens;
using FoundersPC.Domain.Entities.Identity.Users;
using FoundersPC.SharedKernel.ApplicationConstants;
using Microsoft.Extensions.Options;

#endregion

namespace IdentityServer.Tests.DataCreation;

public static class IdentityServerDataCreation
{
    public static readonly Faker<ApplicationUser> UsersFaker;

    public static readonly Faker<AccessToken> AccessTokensFaker;

    static IdentityServerDataCreation()
    {
        var passwordEncryptorService = new PasswordEncryptorService(Options.Create(new PasswordSettings
                                                                                   {
                                                                                       Salt = "1A133311-1178-203C-M1T6-1234QWERTY99",
                                                                                       WorkFactor = 12
                                                                                   }));

        var tokenEncryptionService = new TokenEncryptorService();

        AccessTokensFaker = new Faker<AccessToken>()
            .RuleFor(x => x.Token, tokenEncryptionService.CreateToken());

        UsersFaker = new Faker<ApplicationUser>()
                     .RuleFor(x => x.Email,
                              faker => faker.Person.Email)
                     .RuleFor(x => x.PasswordHash,
                              faker => passwordEncryptorService.EncryptPassword(faker.Random.AlphaNumeric(faker.Random.Int(6, 30))))
                     .RuleFor(x => x.IsActive,
                              faker => faker.Random.Bool(0.97f))
                     .RuleFor(x => x.IsBlocked,
                              (faker, user) => !user.IsActive && faker.Random.Bool(0.9f))
                     .RuleFor(x => x.Login,
                              faker => faker.Person.UserName)
                     .RuleFor(x => x.RegistrationDate,
                              faker => faker.Date.Between(new(2019, 1, 1), DateTime.Now))
                     .RuleFor(x => x.RoleId,
                              faker => faker.PickRandomParam(1, 2, 3))
                     .RuleFor(x => x.SendMessageOnApiRequest,
                              faker => faker.Random.Bool())
                     .RuleFor(x => x.SendMessageOnEntrance,
                              faker => faker.Random.Bool())
                     .RuleFor(x => x.Tokens,
                              (faker, user) => AccessTokensFaker
                                               .RuleFor(x => x.StartEvaluationDate,
                                                        faker2 => faker2.Date.Between(user.RegistrationDate, DateTime.Now))
                                               .RuleFor(x => x.ExpirationDate,
                                                        (faker2, token) => faker2.Date.Soon(faker2.Random.Int(300, 5000), token.StartEvaluationDate))
                                               .RuleFor(x => x.IsBlocked, faker2 => faker2.Random.Bool(0.03f))
                                               .Generate(faker.Random.Int(0, 50)));
    }

    public static IEnumerable<ApplicationRole> GenerateRolesWithData() =>
        new[]
        {
            new ApplicationRole
            {
                Name = ApplicationRoles.Administrator,
                Users = UsersFaker.Generate(1)
            },
            new ApplicationRole
            {
                Name = ApplicationRoles.Manager,
                Users = UsersFaker.Generate(3)
            },
            new ApplicationRole
            {
                Name = ApplicationRoles.DefaultUser,
                Users = UsersFaker.Generate(20)
            }
        };
}