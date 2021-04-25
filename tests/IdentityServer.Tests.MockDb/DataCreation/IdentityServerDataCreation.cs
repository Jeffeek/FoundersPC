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

            UsersFaker = new Faker<UserEntity>()
                         .RuleFor(x => x.Email, faker => faker.Person.Email)
                         .RuleFor(x => x.HashedPassword, faker => passwordEncryptorService.EncryptPassword(faker.Random.AlphaNumeric(faker.Random.Int(6, 30))))
                         .RuleFor(x => x.IsActive, faker => faker.Random.Bool(0.9f))
                         .RuleFor(x => x.IsBlocked, (faker, user) => !user.IsActive && faker.Random.Bool(0.7f))
                         .RuleFor(x => x.Login, faker => faker.Person.UserName)
                         .RuleFor(x => x.RegistrationDate, faker => faker.Date.Between(new DateTime(2019, 1, 1), DateTime.Now))
                         .RuleFor(x => x.RoleId, faker => faker.PickRandomParam(1, 2, 3))
                         .RuleFor(x => x.SendMessageOnApiRequest, faker => faker.Random.Bool())
                         .RuleFor(x => x.SendMessageOnEntrance, faker => faker.Random.Bool());

            UsersEntrancesFaker = new Faker<UserEntranceLog>()
                                  .RuleFor(x => x.User, UsersFaker.Generate())
                                  .RuleFor(x => x.Entrance, (faker, entrance) => faker.Date.Between(entrance.User.RegistrationDate, DateTime.Now));

            AccessTokensFaker = new Faker<AccessTokenEntity>()
                                .RuleFor(x => x.User, UsersFaker.Generate())
                                .RuleFor(x => x.StartEvaluationDate, faker => faker.Date.Between(new DateTime(2010, 1, 1), DateTime.Now))
                                .RuleFor(x => x.ExpirationDate, (faker, token) => faker.Date.Future(faker.Random.Int(1, 100), token.StartEvaluationDate))
                                .RuleFor(x => x.HashedToken, tokenEncryptionService.CreateToken());

            AccessTokenLogsFaker = new Faker<AccessTokenLog>()
                                   .RuleFor(x => x.AccessTokenEntity, AccessTokensFaker.Generate())
                                   .RuleFor(x => x.RequestDateTime,
                                            (faker, log) => faker.Date.Between(log.AccessTokenEntity.StartEvaluationDate,
                                                                               log.AccessTokenEntity.ExpirationDate));
        }

        public static IEnumerable<RoleEntity> GenerateRoles() =>
            new[]
            {
                new()
                {
                    RoleTitle = ApplicationRoles.Administrator
                },
                new RoleEntity
                {
                    RoleTitle = ApplicationRoles.Manager
                },
                new RoleEntity
                {
                    RoleTitle = ApplicationRoles.DefaultUser
                }
            };
    }
}