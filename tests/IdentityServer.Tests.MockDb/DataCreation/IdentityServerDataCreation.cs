#region Using namespaces

using System;
using System.Collections.Generic;
using Bogus;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.Identity.Domain.Entities.Logs;
using FoundersPC.Identity.Domain.Entities.Tokens;
using FoundersPC.Identity.Domain.Entities.Users;
using FoundersPC.Identity.Services.Encryption_Services;

#endregion

namespace IdentityServer.Tests.MockDb.DataCreation
{
    public static class IdentityServerDataCreation
    {
        public static readonly Faker<UserEntity> UsersFaker;

        public static readonly Faker<UserEntranceLog> UsersEntrancesFaker;

        public static readonly Faker<AccessTokenLog> AccessTokenLogsFaker;

        public static readonly Faker<AccessTokenEntity> AccessTokensFaker;

        static IdentityServerDataCreation()
        {
            var passwordEncryptorService = new PasswordEncryptorService();

            var tokenEncryptionService = new TokenEncryptorService();

            UsersEntrancesFaker = new Faker<UserEntranceLog>();

            AccessTokenLogsFaker = new Faker<AccessTokenLog>();

            AccessTokensFaker = new Faker<AccessTokenEntity>()
                .RuleFor(x => x.HashedToken, tokenEncryptionService.CreateToken());

            UsersFaker = new Faker<UserEntity>()
                         .RuleFor(x => x.Email,
                                  faker => faker.Person.Email)
                         .RuleFor(x => x.HashedPassword,
                                  faker => passwordEncryptorService.EncryptPassword(faker.Random.AlphaNumeric(faker.Random.Int(6, 30))))
                         .RuleFor(x => x.IsActive,
                                  faker => faker.Random.Bool(0.97f))
                         .RuleFor(x => x.IsBlocked,
                                  (faker, user) => !user.IsActive && faker.Random.Bool(0.9f))
                         .RuleFor(x => x.Login,
                                  faker => faker.Person.UserName)
                         .RuleFor(x => x.RegistrationDate,
                                  faker => faker.Date.Between(new DateTime(2019, 1, 1), DateTime.Now))
                         .RuleFor(x => x.RoleId,
                                  faker => faker.PickRandomParam(1, 2, 3))
                         .RuleFor(x => x.SendMessageOnApiRequest,
                                  faker => faker.Random.Bool())
                         .RuleFor(x => x.SendMessageOnEntrance,
                                  faker => faker.Random.Bool())
                         .RuleFor(x => x.Entrances,
                                  (faker, user) => UsersEntrancesFaker.RuleFor(x => x.Entrance, faker2 => faker2.Date.Between(user.RegistrationDate, DateTime.Now))
                                                                      .Generate(faker.Random.Int(10, 50)))
                         .RuleFor(x => x.Tokens,
                                  (faker, user) => AccessTokensFaker
                                                   .RuleFor(x => x.StartEvaluationDate,
                                                            faker2 => faker2.Date.Between(user.RegistrationDate, DateTime.Now))
                                                   .RuleFor(x => x.ExpirationDate,
                                                            (faker2, token) => faker2.Date.Soon(faker2.Random.Int(300, 5000), token.StartEvaluationDate))
                                                   .RuleFor(x => x.IsBlocked, faker2 => faker2.Random.Bool(0.03f))
                                                   .RuleFor(x => x.UsagesLogs,
                                                            (faker2, token) => AccessTokenLogsFaker.RuleFor(x => x.RequestDateTime,
                                                                                                       faker3 => faker3.Date.Between(token.StartEvaluationDate, DateTime.Now))
                                                                                                   .Generate(faker2.Random.Int(0, 100)))
                                                   .Generate(faker.Random.Int(0, 50)));
        }

        public static IEnumerable<RoleEntity> GenerateRolesWithData() =>
            new[]
            {
                new()
                {
                    RoleTitle = ApplicationRoles.Administrator,
                    Users = UsersFaker.Generate(1)
                },
                new RoleEntity
                {
                    RoleTitle = ApplicationRoles.Manager,
                    Users = UsersFaker.Generate(3)
                },
                new RoleEntity
                {
                    RoleTitle = ApplicationRoles.DefaultUser,
                    Users = UsersFaker.Generate(20)
                }
            };
    }
}