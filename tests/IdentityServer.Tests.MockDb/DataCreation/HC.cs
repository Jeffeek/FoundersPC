#region Using namespaces

using System;
using System.Collections.Generic;
using Bogus;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.Identity.Domain.Entities.Logs;
using FoundersPC.Identity.Domain.Entities.Users;
using FoundersPC.Identity.Services.Encryption_Services;

#endregion

namespace IdentityServer.Tests.MockDb.DataCreation
{
    public static class HC
    {
        public static readonly Faker<UserEntity> UsersFaker;

        public static readonly Faker<UserEntranceLog> UsersEntrancesFaker;

        static HC()
        {
            var passwordEncryptorService = new PasswordEncryptorService();

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